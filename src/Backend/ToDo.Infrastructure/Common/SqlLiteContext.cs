using ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ToDo.Infrastructure.Common
{
    public class SqlLiteContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public string DbPath { get; }

        public SqlLiteContext(IConfiguration configuration)
        {
            DbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration["DbPath"]));
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}