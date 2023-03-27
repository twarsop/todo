using Application.Common.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToDoItemService _toDoItemService;

        public HomeController(ILogger<HomeController> logger, IToDoItemService toDoItemService)
        {
            _logger = logger;
            _toDoItemService = toDoItemService;
        }

        public IActionResult Index()
        {
            return View(_toDoItemService.GetAll());
        }

        public IActionResult AddToDoItem()
        {
            return View("AddEditToDoItem", new ToDoItemViewModel());
        }

        public IActionResult EditToDoItem(int id)
        {
            var toDoItem = _toDoItemService.Get(id);

            return View("AddEditToDoItem", new ToDoItemViewModel { Id = toDoItem.Id, Description = toDoItem.Description});
        }

        public IActionResult DeleteToDoItem(int id)
        {
            _toDoItemService?.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SaveToDoItem(ToDoItemViewModel toDoItemViewModel)
        {
            _toDoItemService.Save(new ToDoItem { 
                Id = toDoItemViewModel.Id, 
                Description = toDoItemViewModel.Description }
            );

            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}