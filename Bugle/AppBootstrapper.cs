using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;
using SevanConsulting.Bugle.DI;
using SevanConsulting.Bugle.Toast;

namespace SevanConsulting.Bugle
{
    public class AppBootstrapper: StructureMapBootstrapper
    {
        private TaskbarIcon _tbIcon;
        private TrayIconViewModel _trayIconVm;


        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //DisplayRootViewFor<MainViewModel>();
            LogManager.GetLog = type => new DebugLog(type);
            var app = sender as Application;
            Debug.Assert(app != null, "Sender should be Application!");

            _tbIcon = (TaskbarIcon)app.FindResource("MainTaskbarIcon");
            _tbIcon.Visibility = Visibility.Visible;
            _tbIcon.ToolTipText = "Testing 1-2-3";

            // We're inside a bootstrapper so we can ask our DI container to start doing some work for us!
            _trayIconVm = RootContainer.GetInstance<TrayIconViewModel>();
            _tbIcon.DataContext = _trayIconVm;

        }

        /// <summary>
        /// Override this to add custom behavior on exit.
        /// </summary>
        /// <param name="sender">The sender.</param><param name="e">The event args.</param>
        protected override void OnExit(object sender, EventArgs e)
        {
            if (_tbIcon != null && !_tbIcon.IsDisposed)
            {
                _tbIcon.Dispose();
            }
            base.OnExit(sender, e);
        }
    }
}