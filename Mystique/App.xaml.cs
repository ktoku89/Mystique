using System;
using System.IO;
using System.Text;
using System.Windows;
using Inscribe;
using Inscribe.Common;
using Inscribe.Core;
using Livet;
using System.Linq;
using System.Diagnostics;
using System.Windows.Threading;


namespace Mystique
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public object ErrHandlerSender { get; private set; }
        public UnhandledExceptionEventArgs ErrHandlerArg { get; private set; }
        public DispatcherUnhandledExceptionEventArgs ErrHandlerArgDispatcher { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.Exit += new ExitEventHandler(Exitting);
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Current_UnhandledException);
            DispatcherHelper.UIDispatcher = Dispatcher;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Initializer.Init();
        }

        void Exitting(object sender, ExitEventArgs e)
        {
            ThreadHelper.HaltThreads();
        }

        //集約エラーハンドラ
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.ErrHandlerSender = sender;
            this.ErrHandlerArg = e;
            Debugger.Break();
        }

        private void Current_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.ErrHandlerSender = sender;
            this.ErrHandlerArgDispatcher = e;
            Debugger.Break();
        }
    }
}
