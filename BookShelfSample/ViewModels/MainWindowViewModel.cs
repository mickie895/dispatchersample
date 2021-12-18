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

        private BookListDispatcher bookListDispatcher;

        public MainWindowViewModel()
        {
            bookListDispatcher = new BookListDispatcher();
            bookListDispatcher.BookListChanged += BookListCreated;
        }

        private void BookListCreated(object sender, System.EventArgs e)
        {
            BookList = bookListDispatcher.BookList;
            Title = $"本の総数：{BookList.Count}";
        }
    }
}
