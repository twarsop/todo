namespace ToDo.Domain.Entities;

public class ToDoItem
{
    public int Id { get; set; }
    public string Description { get; private set; }

    public ToDoItem(string description)
    {
        Validate(description);
        Description = description;
    }

    public ToDoItem(int id, string description)
        : this(description)
    {
        Id = id;
    }

    public void UpdateDescription(string description)
    {
        Validate(description);
        Description = description;
    }

    private void Validate(string description)
    {
        if (description == null)
        {
            throw new ArgumentNullException("ToDoItem.Description cannot be null.");
        }

        if (description == string.Empty)
        {
            throw new ArgumentException("ToDoItem.Description cannot be empty.");
        }
    }
}