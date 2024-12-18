using DinkToPdf;
using DinkToPdf.Contracts;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Reflection;
using System.Text;

namespace ScanDetector.Service.Implementations
{
    public class PdfReportByDinkToPdf 
    {
        private readonly IConverter _converter;

        public PdfReportByDinkToPdf(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] Generate<T>(IEnumerable<T> items, Dictionary<string, string> headers, string title, string cultureCode)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("The item list is empty.", nameof(items));
            if (headers == null || !headers.Any())
                throw new ArgumentException("Headers cannot be null or empty.", nameof(headers));

            // Determine text direction based on culture
            bool isRightToLeft = string.Equals(cultureCode, "ar-EG", StringComparison.InvariantCultureIgnoreCase);

            // Generate HTML content
            string htmlContent = BuildHtmlContent(items, headers, title, isRightToLeft);

            // Convert HTML to PDF using DinkToPdf
            var pdfDocument = CreatePdfDocument(htmlContent);
            return _converter.Convert(pdfDocument);
        }

        private string BuildHtmlContent<T>(IEnumerable<T> items, Dictionary<string, string> headers, string title, bool isRightToLeft)
        {
            var htmlContent = new StringBuilder();

            // تحديد الاتجاه بناءً على الثقافة
            string direction = isRightToLeft ? "rtl" : "ltr";
            string textAlign = isRightToLeft ? "right" : "left";

            // إضافة بنية HTML وCSS
            htmlContent.AppendLine("<html>");
            htmlContent.AppendLine($"<head><style>")
                      .AppendLine("body { font-family: Arial, sans-serif; margin: 40px; }")
                      .AppendLine($"body {{ direction: {direction}; text-align: {textAlign}; }}") // الاتجاه النصي الافتراضي
                      .AppendLine(".title { font-size: 16px; font-weight: bold; text-align: center; margin-bottom: 20px; }")
                      .AppendLine("table { width: 100%; border-collapse: collapse; }")
                      .AppendLine("th, td { border: 1px solid black; padding: 8px; text-align: center; vertical-align: middle; }")
                      .AppendLine("th { background-color: lightgray; }")
                      .AppendLine("</style></head>");
            htmlContent.AppendLine("<body>");

            // إضافة العنوان
            htmlContent.AppendLine($"<div class='title'>{title}</div>");

            // إضافة الجدول
            htmlContent.AppendLine("<table>");
            htmlContent.AppendLine("<tr>");
            foreach (var header in (isRightToLeft ? headers.Keys.Reverse() : headers.Keys))
            {
                htmlContent.AppendLine($"<th>{header}</th>");
            }
            htmlContent.AppendLine("</tr>");

            foreach (var item in items)
            {
                htmlContent.AppendLine("<tr>");
                foreach (var propertyName in (isRightToLeft ? headers.Values.Reverse() : headers.Values))
                {
                    string value = GetPropertyValue(item, propertyName)?.ToString() ?? string.Empty;
                    htmlContent.AppendLine($"<td>{value}</td>");
                }
                htmlContent.AppendLine("</tr>");
            }
            htmlContent.AppendLine("</table>");

            htmlContent.AppendLine("</body>");
            htmlContent.AppendLine("</html>");
            return htmlContent.ToString();
        }
        private HtmlToPdfDocument CreatePdfDocument(string htmlContent)
        {
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Out = null
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = "" }
                    }
                }
            };
            return pdf;
        }
        private object GetPropertyValue<T>(T item, string propertyName)
        {
            try
            {
                if (item is JObject jObject)
                {
                    // Handle JObject case
                    var token = jObject.SelectToken(propertyName);
                    Log.Information($"PropertyName: {propertyName}, Value: {token}");
                    return token?.ToString();
                }
                else if (item is IDictionary<string, object> dictionary)
                {
                    // Handle Dictionary<string, object> case
                    if (dictionary.TryGetValue(propertyName, out var value))
                    {
                        Log.Information($"PropertyName: {propertyName}, Value: {value}");
                        return value;
                    }
                    else
                    {
                        Log.Warning($"Key {propertyName} not found in dictionary.");
                        return null;
                    }
                }
                else
                {
                    // Handle strongly-typed objects
                    var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (property == null)
                    {
                        Log.Warning($"Property {propertyName} not found on type {typeof(T)}");
                        return null;
                    }

                    var value = property.GetValue(item, null);
                    Log.Information(value != null ? $"PropertyName: {propertyName}, Value: {value}" : $"PropertyName: {propertyName} is null.");
                    return value;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error retrieving property {propertyName}");
                return null;
            }
        }

    }

}
