namespace ToDo.Domain.Entities;

public class ToDoItem
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; private set; }
    public string Description { get; private set; }
    public bool Completed { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public ToDoItem(string description)
    {
        Validate(description);
        Description = description;
    }

    public ToDoItem(Guid id, string description)
        : this(description)
    {
        Id = id;
    }

    public void UpdateDescription(string description)
    {
        Validate(description);
        Description = description;
    }

    public void Complete()
    {
        Completed = true;
        CompletedAt = DateTime.Now;
    }

    public void UnComplete()
    {
        Completed = false;
        CompletedAt = null;
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