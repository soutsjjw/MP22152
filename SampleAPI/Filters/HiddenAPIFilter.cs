using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using SampleAPI.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SampleAPI.Filters;

public class HiddenAPIFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.TryGetMethodInfo(out MethodInfo method))
            {
                if (method != null && method.ReflectedType != null && (method.ReflectedType.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenAPIAttribute))
                        || method.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenAPIAttribute))))
                {
                    string key = "/" + apiDescription.RelativePath;
                    if (key.Contains("?"))
                    {
                        int idx = key.IndexOf("?", System.StringComparison.Ordinal);
                        key = key.Substring(0, idx);
                    }
                    swaggerDoc.Paths.Remove(key);
                }
            }
        }
    }
}