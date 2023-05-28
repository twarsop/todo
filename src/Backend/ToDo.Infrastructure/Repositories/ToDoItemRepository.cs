using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Common;
using Microsoft.Extensions.Configuration;

namespace ToDo.Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly IConfiguration _configuration;

        public ToDoItemRepository(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        
        public ToDoItem Create(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext(_configuration))
            {
                db.Add(toDoItem);
                db.SaveChanges();
                return toDoItem;
            }
        }
        public ToDoItem? Read(int id)
        {
            using (var db = new SqlLiteContext(_configuration))
            {
                return db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();
            }
        }

        public List<ToDoItem> ReadAll()
        {
            using (var db = new SqlLiteContext(_configuration))
            {
                return db.ToDoItems.ToList();
            }
        }
        public void Update(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext(_configuration))
            {
                db.Entry(toDoItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext(_configuration))
            {
                db.Remove(toDoItem);
                db.SaveChanges();
            }
        }
    }
}
