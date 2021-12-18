using BookShelfSample.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public List<BookInfo> BookList { get; set; }

        public MainWindowViewModel()
        {
            BookList = new List<BookInfo>();
            AddItemToShelf();

        }

        public async void AddItemToShelf()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                string bookName = $"TaskName{i}";
                tasks.Add(Task.Run(() => BookList.Add(new BookInfo(bookName))));
            }

            await Task.WhenAll(tasks);

        }
    }
}
