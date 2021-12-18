using System;
using System.Threading;
using System.Windows.Threading;

namespace BookShelfSample.Threading
{
    public abstract class DispatcherBase
    {
        // https://espresso3389.hatenablog.com/entry/2016/08/09/022724 の、
        // 「Dispatcherの闇」の解決を試みる

        private Dispatcher dispatcher = null;
        private Dispatcher baseDispatcher = null;

        public bool Initiallized => dispatcher != null;

        /// <summary>
        /// Dispatcherの初期化処理
        /// </summary>
        protected DispatcherBase()
        {
            baseDispatcher = Dispatcher.CurrentDispatcher;

            baseDispatcher.ShutdownStarted += (s, e) =>
                dispatcher?.BeginInvokeShutdown(DispatcherPriority.Normal);

            var thread = new Thread(
                new ThreadStart(
                    () =>
                    {
                        dispatcher = Dispatcher.CurrentDispatcher;
                        baseDispatcher.BeginInvoke((Action)OnInitiallized);
                        Dispatcher.Run();
                    })
                );
            thread.Start();
        }

        protected void PostOnBaseThread(Action action)
        {
            baseDispatcher.BeginInvoke(action);
        }

        protected void PostOnIsolatedThread(Action action)
        {
            dispatcher.BeginInvoke(action);
        }

        protected abstract void OnInitiallized();
    }
}
