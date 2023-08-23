using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

public class JwtExpirationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    public JwtExpirationMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"])),
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // 验证过期时间
                if (validatedToken.ValidTo >= DateTime.UtcNow)
                {
                    // JWT 有效，继续处理请求，让用户访问其他页面
                    await _next(context);
                    return;
                }
            }
            catch (Exception)
            {
                // JWT 无效或过期，重定向到登录页面
                context.Response.Redirect("/Account/Login");
                return;
            }
        }

        // JWT 不存在或无效，重定向到登录页面
        context.Response.Redirect("/Account/Login");
    }
}
