using CoddingAssessmentProject.Services.Utilities;
using Microsoft.AspNetCore.Http;

namespace CoddingAssessmentProject.Web.Middlewares;

public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;

    public JwtTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = CookieUtils.GetJWTToken(context.Request);

        if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Request.Headers.Add("Authorization", $"Bearer {token}");
        }

        await _next(context);
    }
}
