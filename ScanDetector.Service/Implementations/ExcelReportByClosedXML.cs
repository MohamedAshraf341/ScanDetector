using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ScanDetector.Service.Implementations
{
    public class ExcelReportByClosedXML
    {
        public byte[] Generate<T>(IEnumerable<T> items, Dictionary<string, string> headers, string title, string cultureCode)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("The item list is empty.", nameof(items));
            if (headers == null || !headers.Any())
                throw new ArgumentException("Headers cannot be null or empty.", nameof(headers));

            // Determine text direction based on culture
            bool isRightToLeft = string.Equals(cultureCode, "ar-EG", StringComparison.InvariantCultureIgnoreCase);

            // Create Excel document
            var workbook = GenerateWorkbook(items, headers, title, isRightToLeft);

            // Save and return the Excel file as byte array
            using (var memoryStream = new System.IO.MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private XLWorkbook GenerateWorkbook<T>(IEnumerable<T> items, Dictionary<string, string> headers, string title, bool isRightToLeft)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Report");

            // Set the title and merge cells across the headers
            var titleCell = worksheet.Cell(1, 1);
            titleCell.Value = title;
            titleCell.Style.Font.Bold = true;
            titleCell.Style.Font.FontSize = 14;
            titleCell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            titleCell.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;

            // Merge the title cell across all header columns
            var headerColumnCount = headers.Count;
            worksheet.Range(1, 1, 1, headerColumnCount).Merge().Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

            // Add headers
            var rowIndex = 2;
            foreach (var header in (isRightToLeft ? headers.Keys.Reverse() : headers.Keys))
            {
                worksheet.Cell(rowIndex, headers.Keys.ToList().IndexOf(header) + 1).Value = header;
                worksheet.Cell(rowIndex, headers.Keys.ToList().IndexOf(header) + 1).Style.Font.Bold = true;
                worksheet.Cell(rowIndex, headers.Keys.ToList().IndexOf(header) + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell(rowIndex, headers.Keys.ToList().IndexOf(header) + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
            }

            // Add rows of data
            rowIndex++;
            foreach (var item in items)
            {
                var columnIndex = 1;
                foreach (var propertyName in headers.Values)
                {
                    string value = GetPropertyValue(item, propertyName)?.ToString() ?? string.Empty;
                    worksheet.Cell(rowIndex, columnIndex).Value = value;
                    worksheet.Cell(rowIndex, columnIndex).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(rowIndex, columnIndex).Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                    columnIndex++;
                }
                rowIndex++;
            }

            // Apply table formatting
            var range = worksheet.Range(2, 1, rowIndex - 1, headerColumnCount);
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            range.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            worksheet.Columns().AdjustToContents();
            foreach (var col in worksheet.Columns())
            {
                if (col.Width > 30)
                {
                    col.Width = 40; // Set max width
                }
            }
            worksheet.SetShowGridLines(false);

            return workbook;
        }

        private object GetPropertyValue<T>(T item, string propertyName)
        {
            try
            {
                if (item is JObject jObject)
                {
                    // Handle JObject case
                    var token = jObject.SelectToken(propertyName);
                    return token?.ToString();
                }
                else if (item is IDictionary<string, object> dictionary)
                {
                    // Handle Dictionary<string, object> case
                    if (dictionary.TryGetValue(propertyName, out var value))
                    {
                        return value;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    // Handle strongly-typed objects
                    var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (property == null)
                    {
                        return null;
                    }

                    return property.GetValue(item, null);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
