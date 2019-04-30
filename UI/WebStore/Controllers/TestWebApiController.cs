using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class TestWebApiController : Controller
    {
        private readonly IValuesService _ValuesService;

        public TestWebApiController(IValuesService ValuesService)
        {
            _ValuesService = ValuesService;
        }

        public IActionResult Index()
        {
            return View(_ValuesService.Get());
        }
    }
}