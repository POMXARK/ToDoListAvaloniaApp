

using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ToDoListAvaloniaApp.Models;

namespace ToDoListAvaloniaApp.ViewModels
{
    class AddItemViewModel : ViewModelBase
    {
        [Reactive] 
        public string Description { get; set; }




        public AddItemViewModel()
        {

            // позволяют получать уведомления при изменении свойств объектов -> слушатель
            var okEnabled = 
                this.WhenAnyValue(
                x => x.Description,
                // наблюдаемое значение не пусто и не отсутсвует
                        x => !string.IsNullOrWhiteSpace(x));

            // синхронная команда создать, выполнится асинхронно
            Ok = ReactiveCommand.Create(
                // лямда функция // анонимная
                  () => 
                                // создать новый элемент 
                                new TodoItem { Description = Description },

                // вызвать событие, если true
                    okEnabled
                );

            // синхронная команда пустая?
            Cancel = ReactiveCommand.Create(() => { });

            //// подписать по истечению таймера
            //Observable.Timer(TimeSpan.FromSeconds(2))
            //   // получать уведомления при каждом изменении значения -> вывод наблюдаемого значения
            //   .Subscribe(x => IsChecked = true);




        }



        // Unit аналог void, обьявление команд
        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
