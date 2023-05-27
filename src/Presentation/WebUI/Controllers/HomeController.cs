using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ToDo.Shared.Dtos;
using WebUI.Enums;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string BaseApiUrl = "https://localhost:7180/api/todoitems";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private async Task<HttpResponseMessage> RetryApiEndpoint(HttpMethodEnum httpMethodEnum, string url)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        switch (httpMethodEnum)
                        {
                            case HttpMethodEnum.Get:
                                return await client.GetAsync(url);
                            case HttpMethodEnum.Delete:
                                return await client.DeleteAsync(url);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }

            return null;
        }

        public async Task<IActionResult> Index()
        {
            List<ToDoItemDto> toDoItemsDtos = null;

            var getAllResponse = await RetryApiEndpoint(HttpMethodEnum.Get, BaseApiUrl);

            if (getAllResponse != null && getAllResponse.IsSuccessStatusCode)
            {
                var data = await getAllResponse.Content.ReadAsStringAsync();
                toDoItemsDtos = JsonConvert.DeserializeObject<List<ToDoItemDto>>(data);
            }

            return View(toDoItemsDtos?.Select(x => new ToDoItemViewModel { Id = x.Id, Description = x.Description }).ToList());
        }

        public IActionResult AddToDoItem()
        {
            return View("AddEditToDoItem", new ToDoItemViewModel());
        }

        public IActionResult EditToDoItem(int id)
        {
            // var toDoItem = _toDoItemService.Get(id);
            // return View("AddEditToDoItem", new ToDoItemViewModel { Id = toDoItem.Id, Description = toDoItem.Description});

            return View("AddEditToDoItem", new ToDoItemViewModel());
        }

        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var deleteResponse = await RetryApiEndpoint(HttpMethodEnum.Delete, $"{BaseApiUrl}/{id}");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SaveToDoItem(ToDoItemViewModel toDoItemViewModel)
        {
            //_toDoItemService.Save(toDoItemViewModel.Id, 
            //    toDoItemViewModel.Description
            //);

            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}