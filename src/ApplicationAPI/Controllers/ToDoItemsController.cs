using Application.Common.Interfaces;
using ApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/todoitems")]
    public class ToDoItemsController  : ControllerBase
    {
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemsController(ILogger<ToDoItemsController> logger, IToDoItemService toDoItemService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _toDoItemService = toDoItemService ?? throw new ArgumentNullException(nameof(toDoItemService));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<ToDoItemDto>> Get()
        {
            return Ok(_toDoItemService.GetAll().Select(x => 
                new ToDoItemDto
                {
                    Id = x.Id,
                    Description = x.Description
                })
                .ToList());
        }

        [HttpGet("{id}", Name = "GetToDoItem")]
        public ActionResult<ToDoItemDto> GetToDoItem(int id)
        {
            try
            {
                var toDoItem = _toDoItemService.Get(id);

                if (toDoItem == null)
                {
                    _logger.LogInformation($"To Do Item with id {id} wasn't found");
                    return NotFound();
                }

                return Ok(new ToDoItemDto
                    {
                        Id = toDoItem.Id,
                        Description = toDoItem.Description
                    });
            }
            catch (Exception ex) 
            {
                _logger.LogCritical($"Exception while getting To Do Item for id {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        public ActionResult<ToDoItemDto> CreateToDoItem(ToDoItemForCreationDto toDoItemForCreation)
        {
            var createdToDoItem = _toDoItemService.Create(toDoItemForCreation.Description);

            return CreatedAtRoute("GetToDoItem", 
                new { id = createdToDoItem.Id },
                new ToDoItemDto
                {
                    Id = createdToDoItem.Id,
                    Description = createdToDoItem.Description
                });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateToDoItem(int id, ToDoItemForUpdateDto toDoItemForUpdate)
        {
            if (!_toDoItemService.Update(id, toDoItemForUpdate.Description))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteToDoItem(int id) 
        {
            if (!_toDoItemService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
