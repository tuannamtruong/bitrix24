using bitrix24.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace bitrix24.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostIt()
        {
            return View();
        }

        public IActionResult Caller(int inx)
        {
            
            return View(inx);
        }

        public IActionResult DoIt(int inx)
        {
            var employee = HomeController.listEmployee.result[inx];
            return View(employee);
        }
    }
}
