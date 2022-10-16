using MVC_EmployeeApp.Models;
using System.Collections.Generic;

namespace MVC_EmployeeApp.Repository
{
    public interface IEmployeeRL
    {
        public EmployeeModel AddEmployee(EmployeeModel employee);
        public EmployeeModel UpdateEmployee(EmployeeModel employee, int EmployeeId);
        public List<EmployeeModel> GetAllEmployees();


    }
}
