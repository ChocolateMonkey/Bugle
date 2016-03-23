using Caliburn.Micro;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Media;

namespace SevanConsulting.Bugle.Toast
{
    public class TrayIconViewModel: PropertyChangedBase, IHandle<string>
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

        public void DoubleClickTrayIcon()
        {
            
        }

        public void Handle(string message)
        {
            // handle events from the TFS event manager
            _logger.Info("Event: "+message);

            //todo - need to wrap TrayIcon into something that can be poked from a VM, 'cos this isn't the right direction!
            var viewModel = IoC.Get<BrokenBuildViewModel>();
            var view = ViewLocator.LocateForModel(viewModel, null, null);
            ViewModelBinder.Bind(viewModel, view, null);
            
            PopupControl = view;
            NotifyOfPropertyChange(() => PopupControl);
        }
    }
}