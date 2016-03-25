using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;
using SevanConsulting.Bugle.Toast;

namespace SevanConsulting.Bugle
{
    /// <summary>
    /// For binding views and viewmodels
    /// </summary>
    internal class ViewModelPair<T> where T:PropertyChangedBase
    {
        public UIElement View { get; set; }
        public T ViewModel { get; set; }

        public ViewModelPair(T viewModel, UIElement view)
        {
            ViewModel = viewModel;
            View = view;
        }
    }

    /// <summary>
    /// Action Caliburn messages relating to the system tray icon
    /// </summary>
    public class CaliburnTrayMessageDispatcher: IHandle<string>
    {
        private readonly ILog _logger;

        /// <summary>
        /// Reference to the tray icon control we're operating on
        /// </summary>
        public TaskbarIcon TrayIcon { get; set; }

        public CaliburnTrayMessageDispatcher(ILog logger, IEventAggregator aggregator)
        {
            aggregator.Subscribe(this);
            _logger = logger;            
        }

        /// <summary>
        /// Safely operate on a potentially null instance of a tray icon
        /// </summary>
        /// <param name="operation"></param>
        private void TrayAction(Action<TaskbarIcon> operation)
        {
            if (TrayIcon == null) return;
            operation.Invoke(TrayIcon);
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(string message)
        {
            _logger.Info("Handled event: " + message);

            var popup = ResolveView<BrokenBuildViewModel>();

            popup.ViewModel.BuildMessage = "nah, it's bust again. Whatr are we going to do?! nk; sdfgl;kn dsflk;n sdfl;khj dsfl;k\r\nStuff on a new line";
            popup.ViewModel.Heading = "CI Build broken";

            TrayAction(x => x.ShowCustomBalloon(popup.View, PopupAnimation.Slide, 5500));
        }

        /// <summary>
        /// Find a matching view/VM pairing and wire them up using Caliburn. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private ViewModelPair<T> ResolveView<T>() where T:PropertyChangedBase
        {
            var viewModel = IoC.Get<T>();
            var view = ViewLocator.LocateForModel(viewModel, null, null);
            ViewModelBinder.Bind(viewModel, view, null);
            return new ViewModelPair<T>(viewModel, view);
        }
                    
    }
}