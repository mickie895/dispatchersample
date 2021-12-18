using BookShelfSample.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BookShelfSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private List<BookInfo> bookList;
        public List<BookInfo> BookList
        {
            get { return bookList; }
            set { SetProperty(ref bookList, value); }
        }

        private List<BookInfo> books = new List<BookInfo>();

        public MainWindowViewModel()
        {
            Dispatcher uiThread = Dispatcher.CurrentDispatcher;
            BookList = new List<BookInfo>();
            AddItemToShelf();
            uiThread.BeginInvoke(() => {
                BookList = books;
                Title = $"本の総数：{BookList.Count}冊";
            });
        }

        public async void AddItemToShelf()
        {
            books = new List<BookInfo>();
            ConcurrentQueue<BookInfo> createdBookQueue = new ConcurrentQueue<BookInfo>();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                string bookName = $"TaskName{i}";
                tasks.Add(Task.Run(() => createdBookQueue.Enqueue(new BookInfo(bookName))));
            }

            await Task.WhenAll(tasks);

            while(createdBookQueue.TryDequeue(out BookInfo book))
            {
                books.Add(book);
            }

        }
    }
}
