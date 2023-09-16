using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces;

public interface IToDoItemService
{
    Task<ToDoItem> Create(string description);
    Task<ToDoItem?> Read(Guid id);
    Task<IEnumerable<ToDoItem>> ReadAll();
    Task<bool> Update(Guid id, string description);
    Task<bool> Delete(Guid id);
    Task<int> Count();
    Task<DateTime?> Complete(Guid id);
    Task<bool> UnComplete(Guid id);
}
