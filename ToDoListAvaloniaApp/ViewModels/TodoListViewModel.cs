
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using ToDoListAvaloniaApp.Models;
// для коректной работы IObservable<T>.Subscribe
using System.Reactive.Linq;
using System;
using System.Diagnostics;
using DynamicData.Binding;
using DynamicData;

namespace ToDoListAvaloniaApp.ViewModels
{
    /// <summary>
    /// see MainWindowsViewModel
    /// </summary>
    public class TodoListViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsChecked { get; set; }


        public TodoListViewModel(IEnumerable<TodoItem> items)
        {
            Items = new ObservableCollection<TodoItem>(items);

            var myConnection = Items.ToObservableChangeSet();
            myConnection
                .WhenAnyValue(x => x)
                .Subscribe(_ => Debug.WriteLine("myConnection"));

            // позволяют получать уведомления при изменении свойств объектов -> слушатель
            this.WhenAnyValue(x => x.IsChecked)
                // получать уведомления при каждом изменении значения -> вывод наблюдаемого значения
                .Subscribe(x => Debug.WriteLine($"onNext: IsChecked: {x}"));

            // позволяют получать уведомления при изменении свойств объектов -> слушатель
            this.WhenAnyValue(x => x.Items)
                // получать уведомления при каждом изменении значения -> вывод наблюдаемого значения
                .Subscribe(x => Debug.WriteLine($"onNext: Items: {x}"));

        }


        public ObservableCollection<TodoItem> Items { get; set; }

        public void RunTheThing(string parameter)
        {
            return;
        }



    }
}
