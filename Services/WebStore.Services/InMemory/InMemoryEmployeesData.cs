using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;
using WebStore.Services.Map;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employes = new List<Employee>
        {
            new Employee { Id = 1, SurName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 25 },
            new Employee { Id = 2, SurName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 30 },
            new Employee { Id = 3, SurName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 35 },
        };

        public IEnumerable<Employee> GetAll() => _Employes;

        public Employee GetById(int id) => _Employes.FirstOrDefault(e => e.Id == id);

        public void AddNew(EmployeeView employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_Employes.Any(e => e.Id == employee.Id))
                return;

            _Employes.Add(employee.FromViewModel(_Employes.Count == 0 ? 1 : _Employes.Max(e => e.Id) + 1));
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null) return;
            _Employes.Remove(employee);
        }

        public EmployeeView UpdateEmployee(int id, EmployeeView Employee)
        {
            if(Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            var db_employee = GetById(id);
            if (db_employee is null)
                throw new InvalidOperationException($"Сотрудник с идентификатором {id} не найден");

            Employee.MapTo(db_employee);

            return Employee;
        }

        public void SaveChanges() { }
    }
}
