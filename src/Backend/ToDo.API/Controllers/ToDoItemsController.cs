using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDo.Application.Common.Interfaces;
using ToDo.Shared.Dtos;

namespace ToDo.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/todoitems")]
public class ToDoItemsController : ControllerBase
{
    private readonly ILogger<ToDoItemsController> _logger;
    private readonly IToDoItemService _toDoItemService;
    private readonly IMapper _mapper;
    private const int MaxToDoItemPageSize = 20;

    public ToDoItemsController(ILogger<ToDoItemsController> logger, IToDoItemService toDoItemService, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _toDoItemService = toDoItemService ?? throw new ArgumentNullException(nameof(toDoItemService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Get all to do items, implements paging.
    /// </summary>
    /// <param name="pageNumber">The page to return (starts at 1).</param>
    /// <param name="pageSize">The page size to use for paging.</param>
    /// <returns>IEnumerable&lt;ToDoItemDto&gt;</returns>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToDoItemDto>>> Get(int pageNumber = 1, int pageSize = 10)
    {
        if (pageSize > MaxToDoItemPageSize)
        {
            pageSize = MaxToDoItemPageSize;
        }

        var toDoItems = await _toDoItemService.ReadAll(pageNumber, pageSize);

        var paginationMetadata = new PaginationMetadata(await _toDoItemService.Count(), pageSize, pageNumber);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(_mapper.Map<IEnumerable<ToDoItemDto>>(toDoItems));
    }

    /// <summary>
    /// Get a specific to do item by id.
    /// </summary>
    /// <param name="id">the id of the to do item to return.</param>
    /// <returns>ToDoItemDto</returns>
    [HttpGet("{id}", Name = "GetToDoItem")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Create a new to do item.
    /// </summary>
    /// <param name="toDoItemForCreation">The Dto for creating the new to do item.</param>
    /// <returns>ToDoItemDto - the newly created to do item</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ToDoItemDto>> CreateToDoItem(ToDoItemForCreationDto toDoItemForCreation)
    {
        var createdToDoItem = await _toDoItemService.Create(toDoItemForCreation.Description);

        return CreatedAtRoute("GetToDoItem",
            new { id = createdToDoItem.Id },
            _mapper.Map<ToDoItemDto>(createdToDoItem));
    }

    /// <summary>
    /// Update an existing to do item.
    /// </summary>
    /// <param name="id">The id of the to do item to update</param>
    /// <param name="toDoItemForUpdate">The Dto containing the updated information.</param>
    /// <returns>NotFound if the to do item doesn't exist (by id), or NoContent if successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateToDoItem(int id, ToDoItemForUpdateDto toDoItemForUpdate)
    {
        if (!await _toDoItemService.Update(id, toDoItemForUpdate.Description))
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete an existing to do item.
    /// </summary>
    /// <param name="id">The id of the to do item to delete</param>
    /// <returns>NotFound if the to do item doesn't exist (by id), or NoContent if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteToDoItem(int id)
    {
        if (!await _toDoItemService.Delete(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
