using ToDo.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ToDo.Shared.Dtos;
using AutoMapper;
using ToDo.Domain.Entities;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Route("api/todoitems")]
    public class ToDoItemsController  : ControllerBase
    {
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly IToDoItemService _toDoItemService;
        private readonly IMapper _mapper;

        public ToDoItemsController(ILogger<ToDoItemsController> logger, IToDoItemService toDoItemService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _toDoItemService = toDoItemService ?? throw new ArgumentNullException(nameof(toDoItemService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> Get()
        {
            var toDoItems = await _toDoItemService.ReadAll();
            return Ok(_mapper.Map<IEnumerable<ToDoItemDto>>(toDoItems));
        }

        [HttpGet("{id}", Name = "GetToDoItem")]
        public async Task<ActionResult<ToDoItemDto>> GetToDoItem(int id)
        {
            try
            {
                var toDoItem = await _toDoItemService.Read(id);

                if (toDoItem == null)
                {
                    _logger.LogInformation($"To Do Item with id {id} wasn't found");
                    return NotFound();
                }

                return Ok(_mapper.Map<ToDoItemDto>(toDoItem));
            }
            catch (Exception ex) 
            {
                _logger.LogCritical($"Exception while getting To Do Item for id {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> CreateToDoItem(ToDoItemForCreationDto toDoItemForCreation)
        {
            var createdToDoItem = await _toDoItemService.Create(toDoItemForCreation.Description);

            return CreatedAtRoute("GetToDoItem", 
                new { id = createdToDoItem.Id },
                _mapper.Map<ToDoItemDto>(createdToDoItem));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateToDoItem(int id, ToDoItemForUpdateDto toDoItemForUpdate)
        {
            if (!await _toDoItemService.Update(id, toDoItemForUpdate.Description))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToDoItem(int id) 
        {
            if (!await _toDoItemService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
