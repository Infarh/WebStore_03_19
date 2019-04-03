using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ActionTestController : Controller
    {
        public IActionResult Index()
        {
            //return new ContentResult { Content = "Hello World", StatusCode = 202 };
            //return new EmptyResult();
            //return new FileStreamResult();
            //return new StatusCodeResult(404);
            //return new UnauthorizedResult();
            //return new JsonResult(new { Message = "Helo World!", ServerTime = DateTime.Now });
            //return new RedirectResult("www.yandex.ru") { Permanent = true };
            return new ViewResult { StatusCode = 202 };
        }
    }
}