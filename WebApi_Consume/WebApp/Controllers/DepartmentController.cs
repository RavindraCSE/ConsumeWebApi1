using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using WebApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        Uri baseAddress = new Uri("https://localhost:44314/api");
        HttpClient client;
        public DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            List<DepartmentViewModel> modelList = new List<DepartmentViewModel>(); 
            
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList=  JsonConvert.DeserializeObject<List<DepartmentViewModel>>(data);
            }
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create( DepartmentViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(client.BaseAddress + "/Department", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        // Edit the record here
        public ActionResult Edit(int id)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<DepartmentViewModel>(data);

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(DepartmentViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PutAsync(client.BaseAddress + "/Department/"+model.DeptId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit",model);
        }

        public ActionResult Delete(int id)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Department/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<DepartmentViewModel>(data);

            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Department/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<DepartmentViewModel>(data);

            }
            return View(model);
        }
    }
}