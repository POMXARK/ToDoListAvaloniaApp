
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using ToDoListAvaloniaApp.Models;

namespace ToDoListAvaloniaApp.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }


        public string DbPath { get; }

        public ApplicationContext()
        {
            DbPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "ToDoListAvaloniaApp.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
