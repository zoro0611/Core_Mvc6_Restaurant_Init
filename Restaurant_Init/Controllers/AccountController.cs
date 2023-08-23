using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Restaurant_Init.Models;
using Restaurant_Init.Models.DBModels;
using Restaurant_Init.Models.ViewModels;
using Restaurant_Init.Repos;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant_Init.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepos;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IRepository<User> userRepos, IHttpContextAccessor httpContextAccessor)
        {
            _userRepos = userRepos;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            if (token != null)
            {
                //驗證token中的有效時間是否過期，如果過期要重新登入
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var exp = jwtToken.Payload.Exp;
                var now = (int)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;
                if (now > exp)
                {
                    return View();
                }




                return RedirectToAction("BackStage", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsJsonAsync("https://core6jwtlogin20230823225219.azurewebsites.net/api/JwtAuth/Login", model);
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    Response.Cookies.Append("AdminId", model.UserId);
                    Response.Cookies.Append("token", token);
                    return RedirectToAction("BackStage", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "登入失敗，請檢查使用者名稱和密碼");
                    TempData["ErrorMessage"] = "登入失敗，請檢查使用者名稱和密碼";
                    return View("Login", model);
                }
                
            }
            // 驗證失敗，將使用者導回登入頁面
            TempData["ErrorMessage"] = "登入失敗，請檢查使用者名稱和密碼";
            return View("Login", model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // 呼叫 ASP.NET Core 的登出方法
            //await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            ////刪除所有Cookies
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            // 重定向到登出後的頁面
            return RedirectToAction("Index", "Home");
        }
    }
}
