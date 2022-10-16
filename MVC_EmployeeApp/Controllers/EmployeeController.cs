using Microsoft.AspNetCore.Mvc;

using MVC_EmployeeApp.Models;
using MVC_EmployeeApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MVC_EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRL employeeRL;
        public EmployeeController(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }

        public IActionResult ListOfEmployee()
        {
            List<EmployeeModel> emplist = new List<EmployeeModel>();
            emplist = employeeRL.GetAllEmployees().ToList();

            return View(emplist);
        }

        [HttpGet]
        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmp([Bind] EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeRL.AddEmployee(employee);
                return RedirectToAction("ListOfEmployee");
            }
            return View(employee);
        }
    }
}
