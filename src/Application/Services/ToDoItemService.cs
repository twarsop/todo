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

        public void Save(ToDoItem toDoItem)
        {
            if (toDoItem.Id == default(int))
            {
                _toDoItemRepository.Create(toDoItem);
            }
            else
            {
                _toDoItemRepository.Update(toDoItem);
            }
        }

        public ToDoItem Get(int id)
        {
            return _toDoItemRepository.Read(id);
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
