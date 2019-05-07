using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, "api/EmployeesApi") { }

        public IEnumerable<Employee> GetAll() => Get<List<Employee>>(ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{ServiceAddress}/{id}");

        public void AddNew(EmployeeView employee) => Post(ServiceAddress, employee);

        public void Delete(int id) => Delete($"{ServiceAddress}/{id}");

        public EmployeeView UpdateEmployee(int id, EmployeeView Employee)
        {
            var response = Put($"{ServiceAddress}/{id}", Employee);
            return response.Content.ReadAsAsync<EmployeeView>().Result;
        }

        public void SaveChanges() { }
    }
}
