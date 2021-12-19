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
        /// 初期化処理が行われているスレッドのDispatcherを使って初期化する
        /// </summary>
        protected DispatcherBase(): this(Dispatcher.CurrentDispatcher)
        {
        }

        /// <summary>
        /// ベースとなるDispatcher(通常はUIスレッドのもの)を渡して初期化する
        /// </summary>
        /// <param name="baseDispatcher">ベースとしたいDispatcher</param>
        protected DispatcherBase(Dispatcher baseDispatcher)
        {
            this.baseDispatcher = baseDispatcher;

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
