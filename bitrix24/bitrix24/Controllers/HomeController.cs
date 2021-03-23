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
        readonly string URL = "https://nam.bitrix24.com/rest/user.get";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private async Task<string> GetRestGETResponse(string URI, string parameter = "")
        {
            string resultJson;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("").Result;
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
        private async Task<string> GetAuthToken()
        {
            AuthenticationToken token;

            string result = await GetRestGETResponse("https://bx-oauth2.aasc.com.vn/bx/oauth2_token/local.6059aaa89c4930.05361744");
            if (string.IsNullOrEmpty(result))
                return null;
            token = JsonConvert.DeserializeObject<AuthenticationToken>(result);
            return token.token;
        }


        private async Task<ListEmployee> GetListEmployee()
        {
            string authToken = await GetAuthToken();
            if (string.IsNullOrEmpty(authToken))
                return null;

            string urlParameters = "?scope=&auth=" + authToken;
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
