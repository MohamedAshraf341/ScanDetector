using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;

namespace ScanDetector.Service.Implementations
{
    public class PdfReportByQuestPDF
    {
        public byte[] Generate<T>(IEnumerable<T> items, Dictionary<string, string> headers, string title, string cultureCode)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("The item list is empty.", nameof(items));
            if (headers == null || !headers.Any())
                throw new ArgumentException("Headers cannot be null or empty.", nameof(headers));

            // Determine text direction based on culture
            bool isRightToLeft = string.Equals(cultureCode, "ar-EG", StringComparison.InvariantCultureIgnoreCase);

            // Generate PDF content
            var document = GenerateDocument(items, headers, title, isRightToLeft);

            // Render PDF
            return document.GeneratePdf();
        }

        private IDocument GenerateDocument<T>(IEnumerable<T> items, Dictionary<string, string> headers, string title, bool isRightToLeft)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.Content().Column(col =>
                    {
                        // Add title
                        col.Item().Text(title)
                            .Style(TextStyle.Default.Size(20).Bold().Color(Colors.Blue.Medium))
                            .AlignCenter();

                        col.Item().PaddingVertical(20);

                        // Add table
                        col.Item().Table(table =>
                        {
                            // Add headers
                            table.ColumnsDefinition(columns =>
                            {
                                foreach (var _ in headers)
                                    columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                foreach (var headerText in headers.Keys)
                                {
                                    header.Cell().Element(CellStyle).Text(headerText)
                                        .Style(TextStyle.Default.Bold());
                                }
                                IContainer CellStyle(IContainer cell) => DefaultCellStyle(cell, QuestPDF.Helpers.Colors.Grey.Lighten3);

                            });

                            // Add rows
                            foreach (var item in items)
                            {
                                foreach (var propertyName in headers.Values)
                                {
                                    string value = GetPropertyValue(item, propertyName)?.ToString() ?? string.Empty;
                                    table.Cell().Element(CellStyle).Text(value);
                                   
                                }
                                IContainer CellStyle(IContainer cell) => DefaultCellStyle(cell, QuestPDF.Helpers.Colors.White);
                            }
                        });

                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                        if (isRightToLeft)
                            page.ContentFromRightToLeft();
                    });
                });
            });
        }

        private IContainer DefaultCellStyle(IContainer cell, string backgroundColor)
        {
            return cell
                .Border(1)
                .BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten1)
                .Background(backgroundColor)
                .PaddingVertical(5)
                .PaddingHorizontal(10)
                .AlignCenter()
                .AlignMiddle();
        }

        private object GetPropertyValue<T>(T item, string propertyName)
        {
            try
            {
                if (item is Newtonsoft.Json.Linq.JObject jObject)
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

