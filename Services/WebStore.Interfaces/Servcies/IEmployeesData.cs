using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Servcies
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void AddNew(EmployeeView employee);

        void Delete(int id);

        EmployeeView UpdateEmployee(int id, EmployeeView Employee);

        void SaveChanges();
    }
}
