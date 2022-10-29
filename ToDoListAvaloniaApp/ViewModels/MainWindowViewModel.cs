using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ToDoListAvaloniaApp.Data;
using ToDoListAvaloniaApp.Models;
using ToDoListAvaloniaApp.Services;

namespace ToDoListAvaloniaApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //public string Greeting => "Welcome to Avalonia!";

        [Reactive] public ViewModelBase Content { get; set; }

        public MainWindowViewModel(Database db)
        {
            Content = new TodoListViewModel(db.GetItems());
        }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (TodoItem)null))
                .Take(1)
                .Subscribe(model =>
                {
                    var db = new ApplicationContext();

                    if (model != null)
                    {
                        db.TodoItems.Add(model);
                        db.SaveChanges();
                    }

                    Content = new TodoListViewModel(db.TodoItems);
                });

            Content = vm;
        }
    }
}
