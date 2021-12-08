using BookShelfSample.Model;
using Prism.Mvvm;
using System.Collections.Generic;

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
            BookList = new List<BookInfo>()
            {
                new BookInfo("1984", true),
                new BookInfo("動物農場"),
                new BookInfo("すばらしい新世界", true),
            };
        }
    }
}
