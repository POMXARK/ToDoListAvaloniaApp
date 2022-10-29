using System;
using System.Reactive.Linq;
using ReactiveUI;
using ToDoListAvaloniaApp.Data;
using ToDoListAvaloniaApp.Models;
using ToDoListAvaloniaApp.Services;

namespace ToDoListAvaloniaApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //public string Greeting => "Welcome to Avalonia!";

        ViewModelBase content;

        public MainWindowViewModel(Database db)
        {
            Content = List = new TodoListViewModel(db.GetItems());
        }


        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; }

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
