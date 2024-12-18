using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ScanDetector.Api.Base
{
    public class EnumWithDescriptionSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                foreach (var value in Enum.GetValues(context.Type))
                {
                    var intValue = (int)value;
                    var name = Enum.GetName(context.Type, value);
                    var field = context.Type.GetField(name);
                    var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
                    var description = displayAttribute?.Description ?? name;

                    // Add the numeric value with the description
                    schema.Enum.Add(new OpenApiInteger(intValue));
                    schema.Description += $"{intValue} = {description}\n";
                }
            }
        }
    }
}
