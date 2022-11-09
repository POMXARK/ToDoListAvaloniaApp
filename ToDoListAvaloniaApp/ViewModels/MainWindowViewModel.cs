using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ToDoListAvaloniaApp.Data;
using ToDoListAvaloniaApp.Models;
using ToDoListAvaloniaApp.Services;

namespace ToDoListAvaloniaApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greetings => "Welcome to Avalonia!";

        [Reactive] 
        public ViewModelBase Content { get; set; }

        [Reactive]
        public bool IsChecked { get; set; }


        private readonly ReadOnlyObservableCollection<FootballPlayer> _availablePlayers;

        public ReadOnlyObservableCollection<FootballPlayer> AvailablePlayers => _availablePlayers;

        public MainWindowViewModel(Database db)
        {
            ReactiveCommand<bool, Unit> command = ReactiveCommand.Create<bool>(
    integers => Debug.WriteLine($"execute (bool): {integers}"));

            // This outputs: 42
            command.Execute(true).Subscribe();

            // происходит заполнение данными
            Content = new TodoListViewModel(db.GetItems());


            // синхронная команда пустая?
            ChengesIsChecked = ReactiveCommand.Create<Unit>(x =>
            {

                Debug.WriteLine($"execute: ChengesIsChecked (Unit): {x.GetType()}");

                var vm = new AddItemViewModel();

                Observable.Merge(
                    vm.Ok,
                    vm.Cancel.Select(x => (TodoItem)null))
                    .Take(1)
                    .Subscribe(model =>
                    {
                        var db = new ApplicationContext();

                        if (model != null)
                        {
                            db.TodoItems.Add(model);
                            db.SaveChanges();
                        }

                        // происходит заполнение данными
                        Content = new TodoListViewModel(db.TodoItems);
                    });

            });
            // .Subscribe(x => Debug.WriteLine($"observer: ChengesIsChecked (Unit): {x}"));


            // позволяют получать уведомления при изменении свойств объектов -> слушатель
            this
                .WhenAnyValue(x => x.Content)
                // обработать только уникальные значения
                .DistinctUntilChanged()
                // получать уведомления при каждом изменении значения -> вывод и обработка наблюдаемого значения
                .Subscribe(x =>
                {
                    Debug.WriteLine($"onNext (Content): {x}");
                }
                );



            CreateFootballerList()
                .Connect()
                .Bind(out _availablePlayers)
                .Subscribe();

        }


        //public void ChengesIsChecked()
        //{
        //    var content = Content.Changed;
        //    var changed = Changed;
           
        //    return;
        //}


        public ReactiveCommand<Unit, Unit> ChengesIsChecked { get; }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(x => (TodoItem)null))
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


        private ISourceList<FootballPlayer> CreateFootballerList()
        {
            var people = new SourceList<FootballPlayer>();
            people.AddRange(new[]
            {
                new FootballPlayer("Hennessey"),
                new FootballPlayer("Chester"),
                new FootballPlayer("Williams"),
            });
            return people;
        }

    }


    /// <summary>
    /// Класс модели
    /// </summary>
    public class FootballPlayer
    {
        public string Name { get; }

        public FootballPlayer(string name)
        {
            Name = name;
        }
    }

}
