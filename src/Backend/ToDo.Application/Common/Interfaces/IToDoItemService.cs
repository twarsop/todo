using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces
{
    public interface IToDoItemService
    {
        public ToDoItem Create(string description);
        public ToDoItem? Get(int id);
        public List<ToDoItem> GetAll();
        public bool Update(int id, string description);
        public bool Delete(int id);
    }
}
