using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace DT.Api.Configuration
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context.Type == null)
                return;

            var excludedProperties = context.Type.GetProperties()
                .Where(t =>
                    t.GetCustomAttribute<SwaggerExcludeAttribute>()
                    != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var name = excludedProperty.Name[0].ToString().ToLower() + excludedProperty.Name.Substring(1);
                if (schema.Properties.ContainsKey(name))
                    schema.Properties.Remove(name);
            }
        }
    }
}
