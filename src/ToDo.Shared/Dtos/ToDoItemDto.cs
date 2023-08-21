namespace ToDo.Shared.Dtos;

public record class ToDoItemDto(Guid Id, DateTime CreatedAt, string Description)
{

}
