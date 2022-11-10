using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;


namespace ToDoListAvaloniaApp.Infrastructure
{
    public static class DynamicDataEx
    {
        // Собственный реактивный метод // устаревший метод старой версии
        public static IObservable<IChangeSet<TObject>> FilterOnObservable<TObject, TValue>(this IObservable<IChangeSet<TObject>> source,
            Func<TObject, IObservable<TValue>> observableSelector,
            Func<TValue, bool> predicate)
        {
            return Observable.Create<IChangeSet<TObject>>(observer =>
            {

                //create a local list to store matching values
                //создаем локальный список для хранения совпадающих значений
                var resultList = new SourceList<TObject>();

                //monitor whether the observable has changed and amend local list accordingly
                //отслеживаем, изменился ли наблюдаемый объект, и вносим соответствующие изменения в локальный список
                source.SubscribeMany(item =>
                {
                    return observableSelector(item)
                        .Subscribe(value =>
                        {

                            var isMatched = predicate(value);
                            if (isMatched)
                            {
                                //prevent duplicates with contains check - otherwise use a source cache
                                //предотвратить дубликаты с проверкой содержимого - в противном случае используйте исходный кеш
                                if (!resultList.Items.Contains(item))
                                {
                                    // Заполнение начального списка
                                    resultList.Add(item);
                                }

                            }
                            else
                            {
                                // Удаление после нажатия на кнопку - кнопка привязана к команде
                                resultList.Remove(item);
                            }
                        });
                }).Subscribe();

                resultList.Connect().SubscribeSafe(observer);

                return new CompositeDisposable();

            });
        }
    }
}
