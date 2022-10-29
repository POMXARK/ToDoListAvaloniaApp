

using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using ToDoListAvaloniaApp.Models;

namespace ToDoListAvaloniaApp.ViewModels
{
    class AddItemViewModel : ViewModelBase
    {
        [Reactive] public string Description { get; set; }

        public AddItemViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x));

            Ok = ReactiveCommand.Create(
                () => new TodoItem { Description = Description },
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }


        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
