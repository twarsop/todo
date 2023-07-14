using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public ToDoItemService(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<ToDoItem> Create(string description)
    {
        return await _toDoItemRepository.Create(new ToDoItem(description));
    }

    public async Task<ToDoItem?> Read(int id)
    {
        return await _toDoItemRepository.Read(id);
    }

    public async Task<IEnumerable<ToDoItem>> ReadAll(int pageNumber = 1, int pageSize = 10)
    {
        return await _toDoItemRepository.ReadAll(pageNumber, pageSize);
    }

    public async Task<bool> Update(int id, string description)
    {
        var toDoItem = await Read(id);

        if (toDoItem == null)
        {
            return false;
        }

        toDoItem.UpdateDescription(description);
        await _toDoItemRepository.Update(toDoItem);

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var toDoItem = await Read(id);

        if (toDoItem == null)
        {
            return false;
        }

        _toDoItemRepository.Delete(id);

        return true;
    }

    public async Task<int> Count()
    {
        return await _toDoItemRepository.Count();
    }
}
