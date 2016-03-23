using Caliburn.Micro;
using System.Windows;

namespace SevanConsulting.Bugle.Toast
{
    public class TrayIconViewModel: PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;
        private readonly ILog _logger;

        public TrayIconViewModel(IWindowManager windowManager, ILog logger)
        {
            _windowManager = windowManager;
            _logger = logger;
        }

        public void ShowWindow()
        {
            _logger.Info("Show window clicked!");
        }

        public void HideWindow()
        {
            _logger.Info("Hide window clicked!");
        }

        public void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}