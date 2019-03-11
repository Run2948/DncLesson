using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace Lesson2.Controllers
{
    //    public class TestController : Controller
    //    {
    //        public IActionResult Hello()
    //        {
    //            return Content("Hello");
    //        }
    //    }

    [Controller]
    public class Test : Controller
    {
        private readonly IMemoryCache _cache;

        public Test(IMemoryCache memoryCache)
        {
            this._cache = memoryCache;   
        }

        public IActionResult Hello(RequestModel request, int? age)
        {
            #region HttpRequest
            //            // Query
            //            var name = Request.Query["name"];
            //            // QueryString
            //            var query = Request.QueryString.Value;
            //            // Form
            //            var username = Request.Form["username"];
            //            // Cookies
            //            var cookie = Request.Cookies["item"];
            //            // Headers
            //            var headers = Request.Headers["salt"];

            #endregion

            #region HttpContext
            //            // byte[]
            //            HttpContext.Session.Set("byte", new byte[] { 1, 2, 3, 4, 5 });
            //            var bytes = HttpContext.Session.Get("byte");
            //            // string
            //            HttpContext.Session.SetString("name", "tom");
            //            var name = HttpContext.Session.GetString("name");
            //            // int
            //            HttpContext.Session.SetInt32("id", 20);
            //            var id = HttpContext.Session.GetInt32("id");
            //            HttpContext.Session.Remove("name");
            //            HttpContext.Session.Clear();

            #endregion

            #region 数据绑定
            // 查询字符串
            var test = Request.Query["test"];
            // 简单类型
            var userAge = age;
            // 自定义类型
            var name = request.Name;

            #endregion

            return Content("Hello");
        }

        public IActionResult Say([FromForm]string name,[FromQuery]int age,[FromHeader] string salt,[FromBody] string content)
        {
            return View();
        }

        public IActionResult ReadSession()
        {
            ISession session = HttpContext.Session;
            session.SetString("key", "value");
            session.Remove("key");
            session.GetString("key");
            session.Clear();
            return Content("ok");
        }

        public IActionResult ReadCache()
        {
            _cache.Set("name","tom");
            _cache.Get("name");

            _cache.Set("age",30);
            _cache.Get("age");

            User tom = new User(){ Name = "admin",Pwd = "123456"};
            _cache.Set<User>("user",tom);
            _cache.Get<User>("user");
            return Content("ok");
        }
    }

        public class User
        {
            public string Name { get; set; }
            public string Pwd { get; set; }
        }

    /// <summary>
    /// 拍卖师控制类
    /// </summary>
    [NonController]
    public class AuctionController
    {

    }   

    public class RequestModel
    {
        public string Name { get; set; }       
    }
}
