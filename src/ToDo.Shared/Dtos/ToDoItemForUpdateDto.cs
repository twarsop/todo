using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Dtos
{
    public class ToDoItemForUpdateDto
    {
        [Required]
        public string Description { get; set; }
    }
}
