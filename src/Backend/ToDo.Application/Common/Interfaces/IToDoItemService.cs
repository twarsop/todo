using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces
{
    public interface IToDoItemService
    {
        public Task<ToDoItem> Create(string description);
        public Task<ToDoItem?> Read(int id);
        public Task<IEnumerable<ToDoItem>> ReadAll();
        public Task<bool> Update(int id, string description);
        public Task<bool> Delete(int id);
    }
}
