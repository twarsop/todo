using ApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}", Name = "GetToDoItem")]
        public ActionResult<ToDoItemDto> GetToDoItem(int id)
        {
            var toDoItem = ToDoItemsDataStore.Current.ToDoItems.FirstOrDefault(t => t.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return Ok(toDoItem);
        }

        [HttpPost]
        public ActionResult<ToDoItemDto> CreateToDoItem(ToDoItemForCreationDto toDoItemForCreation)
        {
            var maxToDoItemId = ToDoItemsDataStore.Current.ToDoItems.Max(t => t.Id);

            var toDoItem = new ToDoItemDto()
            {
                Id = ++maxToDoItemId,
                Description = toDoItemForCreation.Description
            };

            ToDoItemsDataStore.Current.ToDoItems.Add(toDoItem);

            return CreatedAtRoute("GetToDoItem", new { id = maxToDoItemId }, toDoItem);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateToDoItem(int id, ToDoItemForUpdateDto toDoItemForUpdate)
        {
            var toDoItem = ToDoItemsDataStore.Current.ToDoItems.FirstOrDefault(t => t.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            toDoItem.Description = toDoItemForUpdate.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteToDoItem(int id) 
        {
            var toDoItem = ToDoItemsDataStore.Current.ToDoItems.FirstOrDefault(t => t.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            ToDoItemsDataStore.Current.ToDoItems.Remove(toDoItem);

            return NoContent();
        }
    }
}
