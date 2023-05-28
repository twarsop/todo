using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoItemService(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public ToDoItem Create(string description)
        {
            return _toDoItemRepository.Create(new ToDoItem(description));
        }

        public ToDoItem? Get(int id)
        {
            return _toDoItemRepository.Read(id);
        }

        public List<ToDoItem> GetAll()
        {
            return _toDoItemRepository.ReadAll();
        }

        public bool Update(int id, string description)
        {
            var toDoItem = Get(id);

            if (toDoItem == null)
            {
                return false;
            }

            toDoItem.UpdateDescription(description);
            _toDoItemRepository.Update(toDoItem);

            return true;
        }

        public bool Delete(int id)
        {
            var toDoItem = Get(id);

            if (toDoItem == null)
            {
                return false;
            }

            _toDoItemRepository.Delete(toDoItem);

            return true;
        }
    }
}
