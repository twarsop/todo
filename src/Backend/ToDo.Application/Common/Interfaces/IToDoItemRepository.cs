using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces;

public interface IToDoItemRepository
{
    Task<ToDoItem> Create(ToDoItem toDoItem);
    Task<ToDoItem?> Read(Guid id);
    Task<IEnumerable<ToDoItem>> ReadAll();
    Task Update(ToDoItem toDoItem);
    void Delete(Guid id);
    Task<int> Count();
}
