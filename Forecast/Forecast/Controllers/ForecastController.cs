using Forecast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Forecast.Controllers
{
    public class ForecastController : Controller
    {
        // GET: WeatherController
        public async Task<ActionResult> Index()
        {
            string Baseurl = "http://localhost:14700";
            var ForInfo = new List<WForecast>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Weathers");
                if (Res.IsSuccessStatusCode)
                {
                    var WeaResponse = Res.Content.ReadAsStringAsync().Result;
                    ForInfo = JsonConvert.DeserializeObject<List<WForecast>>(WeaResponse);
                }
                return View(ForInfo);
            }
        }

        // GET: WeatherController/Details/5
        public async Task<ActionResult> Details(string city)
        {
            TempData["City"] = city;
            string wid = TempData["City"].ToString();
            WForecast w = new WForecast();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14700/api/Weathers/" + wid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    w = JsonConvert.DeserializeObject<WForecast>(apiResponse);
                }
            }
            return View(w);
        }

        // GET: WeatherController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(WForecast w)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(w), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:14700/api/Weathers/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<WForecast>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string city)
        {
            TempData["City"] = city;
            WForecast w = new WForecast();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14700/api/Weathers/" + city))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    w = JsonConvert.DeserializeObject<WForecast>(apiResponse);
                }
            }
            return View(w);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(WForecast b)
        {
            string wid = TempData["City"].ToString();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:14700/api/Weathers/" + wid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string city)
        {
            TempData["City"] = city;
            WForecast w = new WForecast();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:14700/api/Weathers/" + city))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    w = JsonConvert.DeserializeObject<WForecast>(apiResponse);
                }
            }
            return View(w);
        }
        [HttpPost]

        public async Task<ActionResult> Edit(WForecast w)
        {

            string wid = TempData["CityId"].ToString();
            using (var httpClient = new HttpClient())
            {
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(w), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:14700/api/Weathers/" + wid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    w = JsonConvert.DeserializeObject<WForecast>(apiResponse);

                }
            }
            return RedirectToAction("Index");
        }
    }
}