using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAvaloniaApp.Data;
using ToDoListAvaloniaApp.Models;

namespace ToDoListAvaloniaApp.Services
{
    public class Database
    {
        public IEnumerable<TodoItem> GetItems()
        {
            var db = new ApplicationContext();

            return db.TodoItems;

            //return new List<TodoItem>();

            //return new[]{
            //new TodoItem { Description = "Walk the dog" },
            //new TodoItem { Description = "Buy some milk" },
            //new TodoItem { Description = "Learn Avalonia", IsChecked = true },
        }
    }
}
