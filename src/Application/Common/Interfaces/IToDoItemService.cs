using Domain.Models;

namespace Application.Common.Interfaces
{
    public interface IToDoItemService
    {
        public void Save(int id, string description);
        public ToDoItem Get(int id);
        public List<ToDoItem> GetAll();
        public void Delete(int id);
    }
}
