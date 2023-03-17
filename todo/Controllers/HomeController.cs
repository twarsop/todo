using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var db = new ToDoContext();

            var toDoItems = db.ToDoItems.ToList();

            return View(toDoItems);
        }

        public IActionResult AddToDoItem()
        {
            return View("AddEditToDoItem", new ToDoItem());
        }

        public IActionResult EditToDoItem(int id)
        {
            var toDoItem = new ToDoItem();

            using (var db = new ToDoContext())
            {
                toDoItem = db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();
            }
                
            return View("AddEditToDoItem", toDoItem);
        }

        public IActionResult DeleteToDoItem(int id)
        {
            using (var db = new ToDoContext())
            {
                var toDoItem = db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();
                if (toDoItem != null)
                {
                    db.Remove(toDoItem);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SaveToDoItem(int id, string txtDescription)
        {
            using (var db = new ToDoContext())
            {
                if (id == default(int))
                {
                    var newToDoItem = new ToDoItem { Description = txtDescription };
                    db.Add(newToDoItem);
                }
                else
                {
                    var toDoItem = db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();
                    if (toDoItem != null)
                    {
                        toDoItem.Description = txtDescription;
                    }
                }

                db.SaveChanges();
            }
            
            return Redirect("Index");
        }
    }
}