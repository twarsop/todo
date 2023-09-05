using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Infrastructure.DbItems;

public class ToDoItemDbItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; init; }

    [Required]
    [MaxLength(2000)]
    public string Description { get; set; }

    public bool Completed { get; set; }

    public DateTime? CompletedAt { get; set; }

    public ToDoItemDbItem(string description)
    {
        CreatedAt = DateTime.Now;
        Description = description;
    }

    public ToDoItemDbItem(Guid id, string description)
        : this(description)
    {
        Id = id;
    }
}
