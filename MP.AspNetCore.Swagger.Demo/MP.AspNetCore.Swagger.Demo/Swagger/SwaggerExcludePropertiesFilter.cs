using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MP.AspNetCore.Swagger.Demo.Swagger
{
    public class SwaggerExcludePropertiesFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            foreach (string prop in new[] { "notifications", "valid", "invalid" })
            {
                if (model.Properties is null)
                {
                    continue;
                }
                if (model.Properties.ContainsKey(prop))
                {
                    model.Properties.Remove(prop);
                }
            }
        }
    }
}