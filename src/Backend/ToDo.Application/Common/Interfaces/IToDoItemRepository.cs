using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces
{
    public interface IToDoItemRepository
    {
        ToDoItem Create(ToDoItem toDoItem);
        ToDoItem? Read(int id);
        List<ToDoItem> ReadAll();
        void Update(ToDoItem toDoItem);
        void Delete(ToDoItem toDoItem);
    }
}
