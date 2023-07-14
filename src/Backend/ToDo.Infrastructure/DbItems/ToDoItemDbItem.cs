using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Infrastructure.DbItems;

public class ToDoItemDbItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Description { get; set; }

    public ToDoItemDbItem(string description)
    {
        Description = description;
    }

    public ToDoItemDbItem(int id, string description)
        : this(description)
    {
        Id = id;
    }
}
