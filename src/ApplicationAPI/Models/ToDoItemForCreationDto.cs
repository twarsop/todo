using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Models
{
    public class ToDoItemForCreationDto
    {
        [Required]
        public string Description { get; set; }
    }
}
