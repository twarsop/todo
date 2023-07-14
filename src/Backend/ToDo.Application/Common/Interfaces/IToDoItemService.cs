using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces;

public interface IToDoItemService
{
    Task<ToDoItem> Create(string description);
    Task<ToDoItem?> Read(int id);
    Task<IEnumerable<ToDoItem>> ReadAll(int pageNumber, int pageSize);
    Task<bool> Update(int id, string description);
    Task<bool> Delete(int id);
    Task<int> Count();
}
