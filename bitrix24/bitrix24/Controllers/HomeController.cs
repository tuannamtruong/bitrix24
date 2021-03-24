using bitrix24.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace bitrix24.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Result currentEmployee;
        public static ListEmployee listEmployee;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get REST Response content as JSON object.
        /// </summary>
        /// <param name="BaseAddress">Base URI Address</param>
        /// <param name="parameter">URI Parameters</param>
        private async Task<string> GetRestGETResponse(string BaseAddress, string parameter = "")
        {
            string resultJson;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(parameter).Result;
            if (response.IsSuccessStatusCode)
            {
                resultJson = await response.Content.ReadAsStringAsync();
            }
            else
            {
                resultJson = "";
            }
            client.Dispose();
            return resultJson;
        }

        /// <summary>
        /// Get authentication token.
        /// </summary>
        private async Task<string> GetAuthToken()
        {
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\appid.txt");
            string appId = sr.ReadLine();
            string result = await GetRestGETResponse("https://bx-oauth2.aasc.com.vn/bx/oauth2_token/" + appId);
            if (string.IsNullOrEmpty(result))
                return null;
            var authenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(result);
            return authenticationToken.token;
        }

        /// <summary>
        /// Get Listemployee from bitrix24
        /// </summary>
        /// <returns></returns>
        private async Task<ListEmployee> GetListEmployee()
        {
            string authToken = await GetAuthToken();
            if (string.IsNullOrEmpty(authToken))
                return null;

            string urlParameters = "?scope=&auth=" + authToken;
            string getResult = await GetRestGETResponse("https://nam.bitrix24.com/rest/user.get", urlParameters);
            if (string.IsNullOrEmpty(getResult))
            {
                listEmployee = null;
            }
            else
            {
                listEmployee = JsonConvert.DeserializeObject<ListEmployee>(getResult);
            }
            return listEmployee;
        }


        public IActionResult Index()
        {
            var listEmployee = GetListEmployee().Result;
            if (listEmployee == null)
                return View("Error");
            return View(listEmployee);
        }

        public IActionResult Refresh()
        {
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
