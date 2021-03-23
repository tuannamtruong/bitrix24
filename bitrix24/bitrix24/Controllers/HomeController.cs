using bitrix24.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace bitrix24.Controllers
{
    public class HomeController : Controller
    {
        string URL = "https://nam.bitrix24.com/rest/user.get";
        string urlParameters = "?scope=&auth=b0d7596000533bb900533bb500000001e0e30345b5cf38b573b84323f9d19896500c64";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private async Task<ListEmployee> GetListEmployee()
        {
            ListEmployee listEmployee;
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(URL)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                string getResult = await response.Content.ReadAsStringAsync();
                listEmployee = JsonConvert.DeserializeObject<ListEmployee>(getResult);
            }
            else
            {
                listEmployee = null;
            }
            client.Dispose();
            return listEmployee;
        }
        public IActionResult Index()
        {
            var listEmployee = GetListEmployee().Result;
            if (listEmployee == null)
                return View("Error");
            return View(listEmployee);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
