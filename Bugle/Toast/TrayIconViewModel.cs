using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Media;
using SevanConsulting.Bugle.Services;

namespace SevanConsulting.Bugle.Toast
{
    /// <summary>
    /// View model for system tray icon, serves as application main view / VM.
    /// </summary>
    public class TrayIconViewModel: PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;
        private readonly ILog _logger;
        private readonly IEventAggregator _eventAggregator;

        public UIElement PopupControl { get; set; }

        public TrayIconViewModel(IWindowManager windowManager, ILog logger, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _logger = logger;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void ShowConfiguration()
        {
            var vm = IoC.Get<ConfigurationViewModel>();
            _windowManager.ShowDialog(vm);
        }

        public void ShowAbout()
        {
            var vm = IoC.Get<AboutViewModel>();
            _windowManager.ShowDialog(vm);
        }

        public void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        public void DoubleClickIcon()
        {
            _eventAggregator.PublishOnUIThread("Clicked");
        }

    }
}