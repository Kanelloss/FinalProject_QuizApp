using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QuizApp.Helpers
{
    public class AuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Ελέγχει αν υπάρχουν [Authorize] attributes στον controller ή τη μέθοδο
            var authAttributes = context.MethodInfo
                .DeclaringType?
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Union(context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>())
                .Distinct();

            if (authAttributes != null && authAttributes.Any())
            {
                // Προσθέτει αποκρίσεις 401 (Unauthorized) και 403 (Forbidden)
                if (!operation.Responses.ContainsKey("401"))
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized. You need to authenticate in order to access this endpoint." });
                }

                if (!operation.Responses.ContainsKey("403"))
                {
                    operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden. You do not have permission to access this resource." });
                }

                // Ορίζει το security schema για το JWT
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [jwtSecurityScheme] = Array.Empty<string>() // Χωρίς συγκεκριμένους ρόλους
                    }
                };
            }
        }
    }
}
