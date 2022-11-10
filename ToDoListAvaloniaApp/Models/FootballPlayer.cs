

using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using ToDoListAvaloniaApp.Infrastructure;

namespace ToDoListAvaloniaApp.Models
{
    /// <summary>
    /// Класс модели
    /// </summary>
    public class FootballPlayer
    {
        public ReactiveCommand<Unit, Unit> IncludeCommand { get; }
        public string Name { get; }
        //public ICommand IncludeCommand { get; }
        public IObservable<bool> IncludedChanged { get; }

        public FootballPlayer(string name)
        {
            var includeChanged = new BehaviorSubject<bool>(false);

            Name = name;
            //IncludeCommand = new Command(() => includeChanged.OnNext(true));
            IncludeCommand = ReactiveCommand.Create(() => includeChanged.OnNext(true));
            IncludedChanged = includeChanged.AsObservable();
        }
    }

}
