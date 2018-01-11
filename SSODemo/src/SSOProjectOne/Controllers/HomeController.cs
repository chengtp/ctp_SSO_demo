using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using SSOProjectOne.Common;
using SSOProjectOne.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SSOProjectOne.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            string userCode = string.Empty;
            string totenID = string.Empty;
            if (!string.IsNullOrEmpty(Request.Query["token"]) && Request.Query["token"].Any() && !string.IsNullOrEmpty(Request.Query["userCode"]) && Request.Query["userCode"].Any())
            {
                userCode = Request.Query["userCode"][0];
                totenID = Request.Query["token"][0];

                // HttpContext.Response.Cookies.Append("","");

                var identity = new ClaimsIdentity("UserForm");     // 指定身份认证类型
                identity.AddClaim(new Claim(ClaimTypes.Sid, userCode));  // 键值对
                identity.AddClaim(new Claim(ClaimTypes.Name, totenID));       // 键值对
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.Authentication.SignInAsync("Cookie", principal, new AuthenticationProperties { IsPersistent = true });

            }
            else if (User.FindFirst(ClaimTypes.Sid) != null)
            {
                totenID = User.FindFirst(ClaimTypes.Sid).Value;

            }

            if (!string.IsNullOrEmpty(totenID))
            {
                //远程sso 服务中心验证 数据的有效性
                UserTotenInput userInput = new UserTotenInput()
                {
                    UserCode = userCode,
                    Toten = totenID
                };

                var flag = HttpClientHeaper<UserTotenInput>.Post(StaticParameter.ValidateToken, userInput);
                if (flag == null)
                {
                    //跳转登录页面
                    return Redirect("http://www.baidu.com/Home/index");
                }
            }
            else
            {
                //跳转登录页面

            }



            return View();



        }

        /// <summary>
        /// 没有权限跳转到登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return Redirect("http://www.baidu.com/Home/index");
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
