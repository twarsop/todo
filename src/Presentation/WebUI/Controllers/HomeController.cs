using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using ToDo.Shared.Dtos;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://localhost:7180/api/todoitems";
            List<ToDoItemDto> toDoItemsDtos = null;

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            toDoItemsDtos = JsonConvert.DeserializeObject<List<ToDoItemDto>>(data);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(1000);
                }
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

        public IActionResult DeleteToDoItem(int id)
        {
            // _toDoItemService.Delete(id);

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