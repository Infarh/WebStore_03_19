using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/EmployeesApi")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        /* ----------------------------------------------------------------------------------------------------- */

        [HttpGet, ActionName("Get")]
        public IEnumerable<Employee> GetAll() => _EmployeesData.GetAll();

        [HttpGet("{id}"), ActionName("Get")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody] EmployeeView employee) => _EmployeesData.AddNew(employee);

        [HttpDelete("{id}")]
        public void Delete(int id) => _EmployeesData.Delete(id);

        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeView UpdateEmployee(int id, EmployeeView Employee) =>
            _EmployeesData.UpdateEmployee(id, Employee);

        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}