﻿using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        public void Create(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext())
            {
                db.Add(toDoItem);
                db.SaveChanges();
            }
        }
        public ToDoItem Read(int id)
        {
            var toDoItem = new ToDoItem();
            using (var db = new SqlLiteContext())
            {
                toDoItem = db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();
            }
            return toDoItem;
        }

        public List<ToDoItem> ReadAll()
        {
            using (var db = new SqlLiteContext())
            {
                return db.ToDoItems.ToList();
            }
        }
        public void Update(ToDoItem toDoItem)
        {
            using (var db = new SqlLiteContext())
            {
                var toDoItemUpdate = db.ToDoItems.Where(t => t.Id == toDoItem.Id).FirstOrDefault();
                if (toDoItemUpdate != null)
                {
                    toDoItemUpdate.Description = toDoItem.Description;
                    db.SaveChanges();
                }
            }
        }
        public void Delete(int id)
        {
            using (var db = new SqlLiteContext())
            {
                var toDoItem = db.ToDoItems.Where(t => t.Id == id).FirstOrDefault();
                if (toDoItem != null)
                {
                    db.Remove(toDoItem);
                    db.SaveChanges();
                }
            }
        }
    }
}
