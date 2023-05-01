using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Models
{
    public class ToDoItemForUpdateDto
    {
        [Required]
        public string Description { get; set; }
    }
}
