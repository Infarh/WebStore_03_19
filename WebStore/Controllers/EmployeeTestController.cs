using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class EmployeeTestController : Controller
    {
        // GET: EmployeeTest
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeTest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeTest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeTest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}