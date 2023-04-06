using Domain.Models;

namespace Application.Common.Interfaces
{
    public interface IToDoItemRepository
    {
        void Create(ToDoItem toDoItem);
        ToDoItem? Read(int id);
        List<ToDoItem> ReadAll();
        void Update(ToDoItem toDoItem);
        void Delete(int id);
    }
}
