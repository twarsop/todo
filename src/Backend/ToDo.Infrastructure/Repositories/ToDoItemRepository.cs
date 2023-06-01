using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using ToDo.Infrastructure.DbItems;

namespace ToDo.Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        public ToDoItemRepository() 
        { 

        }
        
        public ToDoItem Create(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext())
            {
                var toDoItemDbItem = new ToDoItemDbItem(toDoItem.Description);

                db.Add(toDoItemDbItem);
                db.SaveChanges();
                return new ToDoItem(toDoItemDbItem.Id, toDoItemDbItem.Description);
            }
        }
        public ToDoItem? Read(int id)
        {
            using (var db = new SqlLiteContext())
            {
                var toDoItemDbItem = db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();

                return toDoItemDbItem == null ? null : new ToDoItem(toDoItemDbItem.Id, toDoItemDbItem.Description);
            }
        }

        public List<ToDoItem> ReadAll()
        {
            using (var db = new SqlLiteContext())
            {
                return db.ToDoItems.Select(x => new ToDoItem(x.Id, x.Description)).ToList();
            }
        }
        public void Update(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext())
            {
                var toDoItemDbItem = new ToDoItemDbItem(toDoItem.Id, toDoItem.Description);
                db.Entry(toDoItemDbItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext())
            {
                var toDoItemDbItem = new ToDoItemDbItem(toDoItem.Id, toDoItem.Description);
                db.Remove(toDoItemDbItem);
                db.SaveChanges();
            }
        }
    }
}
