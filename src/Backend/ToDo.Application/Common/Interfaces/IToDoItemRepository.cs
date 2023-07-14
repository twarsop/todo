using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces;

public interface IToDoItemRepository
{
    Task<ToDoItem> Create(ToDoItem toDoItem);
    Task<ToDoItem?> Read(int id);
    Task<IEnumerable<ToDoItem>> ReadAll(int pageNumber, int pageSize);
    Task Update(ToDoItem toDoItem);
    void Delete(int id);
    Task<int> Count();
}
