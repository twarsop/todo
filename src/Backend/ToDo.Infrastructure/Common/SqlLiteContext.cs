using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDo.Infrastructure.DbItems;

namespace ToDo.Infrastructure.Common;

public class SqlLiteContext : DbContext
{
    public DbSet<ToDoItemDbItem> ToDoItems { get; set; }

    public string DbPath { get; }

    public SqlLiteContext()
    {
        var configDbPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["DbPath"];
        DbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configDbPath));
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}