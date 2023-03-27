using Domain.Models;

namespace Application.Common.Interfaces
{
    public interface IToDoItemService
    {
        void Save(ToDoItem toDoItem);
        public ToDoItem Get(int id);
        public List<ToDoItem> GetAll();
        public void Delete(int id);
    }
}
