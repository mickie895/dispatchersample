using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

namespace DispatcherSample
{
    public abstract class DispatcherSample
    {
        // https://espresso3389.hatenablog.com/entry/2016/08/09/022724 の、
        // 「Dispatcherの闇」の解決を試みる

        protected Dispatcher dispatcher = null;

        public bool Initiallized => dispatcher != null;

        /// <summary>
        /// Dispatcherの初期化処理
        /// </summary>
        protected DispatcherSample()
        {
            Dispatcher baseDispatcher = Dispatcher.CurrentDispatcher;

            baseDispatcher.ShutdownStarted += (s, e) =>
                dispatcher?.BeginInvokeShutdown(DispatcherPriority.Normal);

            var thread = new Thread(
                new ThreadStart(
                    () => {
                        dispatcher = Dispatcher.CurrentDispatcher;
                        baseDispatcher.BeginInvoke((Action)OnInitiallized);
                        Dispatcher.Run();
                    })
                );
            thread.Start();
        }

        protected abstract void OnInitiallized();
    }
}
