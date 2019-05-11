using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Map
{
    public static class EmployeesMapper
    {
        public static void MapTo(this EmployeeView source, Employee destination)
        {
            if(source is null) return;

            destination.FirstName = source.FirstName;
            destination.SurName = source.SurName;
            destination.Patronymic = source.Patronymic;
            destination.Age = source.Age;
        }

        public static Employee FromViewModel(this EmployeeView model, int id = default)
        {
            if (model is null) return null;
            var employee = new Employee { Id = id };
            model.MapTo(employee);
            return employee;
        }
    }
}
