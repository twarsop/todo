using Application.Common.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoItemService(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public void Save(int id, string description)
        {
            if (id == default(int))
            {
                _toDoItemRepository.Create(new ToDoItem(description));
            }
            else
            {
                var toDoItem = Get(id);
                toDoItem.UpdateDescription(description);
                _toDoItemRepository.Update(toDoItem);
            }
        }

        public ToDoItem Get(int id)
        {
            var toDoItem = _toDoItemRepository.Read(id);

            if (toDoItem == null)
            {
                throw new NullReferenceException($"Could not find toDoItem with id={id}");
            }

            return toDoItem;
        }

        public List<ToDoItem> GetAll()
        {
            return _toDoItemRepository.ReadAll();
        }

        public void Delete(int id)
        {
            _toDoItemRepository.Delete(id);
        }
    }
}
