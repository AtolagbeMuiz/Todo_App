using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoAPI.Interfaces;
using ToDoApp.DTOs;
using ToDoApp.Models;
using ToDoApp.Utilities;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClient _client;
        private readonly ApiUri _apiUri;

        public HomeController(ILogger<HomeController> logger, IClient client, IOptionsSnapshot<ApiUri> options)
        {
            _client = client;
            _logger = logger;
            _apiUri = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ToDoDTO todoDTO)
        {
            if (ModelState.IsValid)
            {
                
                var response = await _client.PostAsync(_apiUri.BaseUrl, _apiUri.CreateToDo, todoDTO, "");


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var apiTask = response.Content.ReadAsStringAsync();
                    var responseString = apiTask.Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                    if(apiResponse.Code == "00")
                    {
                        ViewBag.message = "Todo Item created sucessfully";
                        ViewBag.type = "success";
                        return View();

                    }

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.message = "oops, something went wrong try again later";
                    ViewBag.type = "error";
                    return View();
                }
                else
                {
                    ViewBag.message = "oops, something went wrong try again later";
                    ViewBag.type = "error";
                    return View();
                }
                    
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDos()
        {
            if (TempData.ContainsKey("message") && TempData.ContainsKey("type"))
            {
                ViewBag.message = TempData["message"];
                ViewBag.type = TempData["type"];

            }

            var response = await _client.GetAsync(_apiUri.BaseUrl, _apiUri.GetAllToDos, "", "");


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiTask = response.Content.ReadAsStringAsync();
                var responseString = apiTask.Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                if (apiResponse.Code == "00")
                {
                    string json = JsonConvert.SerializeObject(apiResponse.Data);
                    var todos = JsonConvert.DeserializeObject<List<ToDoDTO>>(json);
                    return View(todos);

                }

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                //var todoDTO = null;
                return View();
            }
            else
            {
               
                return View();
            }

            return View();
        }

        public async Task<IActionResult> DeleteTodoItem(string Id)
        {
            var response = await _client.GetAsync(_apiUri.BaseUrl, _apiUri.DeleteToDoItem+"?Id="+Id, "", "");


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiTask = response.Content.ReadAsStringAsync();
                var responseString = apiTask.Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                if (apiResponse.Code == "00")
                {
                    TempData["message"] = "Item Deleted Sucessfully";
                    TempData["type"] = "success";

                    return RedirectToAction("GetAllToDos");

                }

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["message"] = "cannot delete item now, try again later";
                TempData["type"] = "error";

                return RedirectToAction("GetAllToDos"); 
            }
            else
            {
                TempData["message"] = "cannot delete item now, try again later";
                TempData["type"] = "error";

                return RedirectToAction("GetAllToDos");
            }

            TempData["message"] = "cannot delete item now, try again later";
            TempData["type"] = "error";

            return RedirectToAction("GetAllToDos");
        }

        public async Task<IActionResult> EditTodoItem(string Id)
        {
            var response = await _client.GetAsync(_apiUri.BaseUrl, _apiUri.GetToDoItemById + "?Id=" + Id, "", "");


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiTask = response.Content.ReadAsStringAsync();
                var responseString = apiTask.Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                if (apiResponse.Code == "00")
                {
                    string json = JsonConvert.SerializeObject(apiResponse.Data);
                    var todoResponse = JsonConvert.DeserializeObject<ToDoDTO>(json);

                    var todo = new ToDoDTO
                    {
                        Id = todoResponse.Id,
                        Name = todoResponse.Name,
                        Description = todoResponse.Description,
                        Priority = todoResponse.Priority,
                        Status = todoResponse.Status
                    };

                    return View(todo);

                }

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                TempData["message"] = "cannot find item now, try again later";
                TempData["type"] = "error";

                return RedirectToAction("GetAllToDos");
            }
            else
            {
                TempData["message"] = "cannot find item now, try again later";
                TempData["type"] = "error";

                return RedirectToAction("GetAllToDos");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateToDoItem(ToDoDTO todoDTO)
        {
            if (ModelState.IsValid)
            {

                var response = await _client.PostAsync(_apiUri.BaseUrl, _apiUri.UpdateToDoItem, todoDTO, "");


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var apiTask = response.Content.ReadAsStringAsync();
                    var responseString = apiTask.Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                    if (apiResponse.Code == "00")
                    {
                        TempData["message"] = "Todo Item edited sucessfully";
                        TempData["type"] = "success";

                        return RedirectToAction("GetAllToDos");

                    }

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["message"] = "oops, something went wrong try again later";
                    TempData["message"] = "error";
                    return RedirectToAction("GetAllToDos");
                }
                else
                {
                    TempData["message"] = "oops, something went wrong try again later";
                    TempData["message"] = "error";
                    return RedirectToAction("GetAllToDos");
                }

            }
            TempData["message"] = "oops, something went wrong try again later";
            TempData["message"] = "error";
            return RedirectToAction("GetAllToDos");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
