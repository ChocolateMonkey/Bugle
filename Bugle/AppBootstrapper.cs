using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;
using SevanConsulting.Bugle.DI;
using SevanConsulting.Bugle.Services;
using SevanConsulting.Bugle.Toast;

namespace SevanConsulting.Bugle
{
    public class AppBootstrapper: StructureMapBootstrapper
    {
        private TfsEventManager _eventManager;

        static AppBootstrapper() 
        {
            LogManager.GetLog = type => new DebugLog(type);
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<TrayIconViewModel>();
            _eventManager = RootContainer.GetInstance<TfsEventManager>();

        }

        /// <summary>
        /// Override this to add custom behavior on exit.
        /// </summary>
        /// <param name="sender">The sender.</param><param name="e">The event args.</param>
        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }
    }
}