using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Restaurant_Init.Models;
using Restaurant_Init.Models.DBModels;
using Restaurant_Init.Models.ViewModels;
using Restaurant_Init.Repos;
using System.Diagnostics;
using System.Security.Claims;

namespace Restaurant_Init.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepos;

        public AccountController(IRepository<User> userRepos)
        {
            _userRepos = userRepos;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepos.GetAllReadOnly().SingleOrDefault(x => x.UserId == model.UserId && x.Password == model.Password);
                if (user is not null)// 資料庫有對應的使用者資訊，驗證登入通過
                {


                    // 其他需要的使用者資訊，可以在這裡設定
                    Response.Cookies.Append("AdminName", user.Username, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(60)
                    });

                    // 將使用者資訊存入 Session 或其他認證機制中
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    };
                    var identity = new ClaimsIdentity(claims, "MyCookieAuthenticationScheme");

                    await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(identity));

                    // 跳轉到登入成功後的頁面
                    return RedirectToAction("BackStage", "Home");
                }
                else
                {
                    // 資料庫無對應的使用者資訊，驗證登入失敗
                    ModelState.AddModelError("", "登入失敗，請檢查使用者名稱和密碼");
                    TempData["ErrorMessage"] = "登入失敗，請檢查使用者名稱和密碼";
                }
                
                
            }
            // 驗證失敗，將使用者導回登入頁面
            return View("Login", model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // 呼叫 ASP.NET Core 的登出方法
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            //刪除所有Cookies
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            // 重定向到登出後的頁面
            return RedirectToAction("Index", "Home");
        }
    }
}
