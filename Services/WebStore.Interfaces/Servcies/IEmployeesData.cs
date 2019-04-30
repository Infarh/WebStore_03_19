using System.Collections.Generic;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Servcies
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void AddNew(Employee employee);

        void Delete(int id);

        void SaveChanges();
    }
}
