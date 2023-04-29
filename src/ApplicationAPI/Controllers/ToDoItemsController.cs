using ApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/todoitems")]
    public class ToDoItemsController  : ControllerBase
    {
        [HttpGet()]
        public ActionResult<IEnumerable<ToDoItemDto>> Get()
        {
            return Ok(ToDoItemsDataStore.Current.ToDoItems);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItemDto> GetToDoItem(int id)
        {
            var toDoItem = ToDoItemsDataStore.Current.ToDoItems.FirstOrDefault(t => t.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return Ok(toDoItem);
        }
    }
}
