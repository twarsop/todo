﻿namespace ToDo.Mvc.WebUI.Models;

public class ToDoItemViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
}
