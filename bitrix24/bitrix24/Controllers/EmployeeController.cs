using Microsoft.AspNetCore.Mvc;

namespace bitrix24.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetEmployeeInfo(int index)
        {
            var employee = HomeController.listEmployee.result[index];
            return View(employee);
        }
    }
}
