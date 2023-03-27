using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Infrastructure.Common
{
    public class SqlLiteContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public string DbPath { get; }

        public SqlLiteContext()
        {
            DbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings.Get("DbPath")));
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}