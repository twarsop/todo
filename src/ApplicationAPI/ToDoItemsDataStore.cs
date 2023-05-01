using ApplicationAPI.Models;

namespace ApplicationAPI
{
    public class ToDoItemsDataStore
    {
        public List<ToDoItemDto> ToDoItems { get; set; }
        public static ToDoItemsDataStore Current { get; } = new ToDoItemsDataStore();

        public ToDoItemsDataStore() 
        {
            ToDoItems = new List<ToDoItemDto>()
            {
                new ToDoItemDto()
                {
                    Id = 1,
                    Description = "First to do item"
                },
                new ToDoItemDto()
                {
                    Id = 2,
                    Description = "Second to do item"
                },
                new ToDoItemDto()
                {
                    Id = 3,
                    Description = "Third to do item"
                },
            };
        }
    }
}
