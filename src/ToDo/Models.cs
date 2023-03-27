using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

public class ToDoContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public string DbPath { get; }

    public ToDoContext()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        //if "bin" is present, remove all the path starting from "bin" word
        if (baseDir.Contains("bin"))
        {
            int index = baseDir.IndexOf("bin");
            baseDir = baseDir.Substring(0, index);
        }
        
        DbPath = System.IO.Path.Join(baseDir, "todo.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class ToDoItem
{
    public int Id { get; set; }
    public string Description { get; set; }
}