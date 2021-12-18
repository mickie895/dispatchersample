using System;
using System.Collections.Generic;
using System.Text;
using BookShelfSample.Threading;
using BookShelfSample.Model;

namespace BookShelfSample.ViewModels
{
    class BookListDispatcher : DispatcherBase
    {
        public BookListDispatcher(): base()
        {

        }

        public List<BookInfo> BookList;

        public event EventHandler BookListChanged = null;

        private void OnBookListCreateCompleted()
        {
            BookListChanged?.Invoke(this, new EventArgs());
        }

        private void BookListCreateComplete()
        {
            PostOnBaseThread(OnBookListCreateCompleted);
        }

        protected override void OnInitiallized()
        {
            BookList = new List<BookInfo>();
            for (int i = 0; i < 100; i++)
            {
                string bookName = $"QueueName{i}";
                PostOnIsolatedThread((Action)(() => BookList.Add(new BookInfo(bookName))));
            }

            PostOnIsolatedThread((Action)BookListCreateComplete);
        }
    }
}
