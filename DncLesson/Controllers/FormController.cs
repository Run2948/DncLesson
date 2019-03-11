using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson2.Controllers
{
    [Route("admin/form")]
    public class FormController : Controller
    {
        [Route("index")]
        public IActionResult Index([FromRoute] int? id)
        {
            return View(new UserLogin());
        }

        public IActionResult PostData(UserLogin login)
        {
            return Content(ModelState.IsValid?"数据有效":"数据无效");
        }
    }
}