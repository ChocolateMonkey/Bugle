using System;
using System.Windows.Controls.Primitives;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;
using SevanConsulting.Bugle.Toast;

namespace SevanConsulting.Bugle
{
    /// <summary>
    /// Action Caliburn messages
    /// </summary>
    public class CaliburnMessageDispatcher: IHandle<string>
    {
        private readonly ILog _logger;

        /// <summary>
        /// Reference to the tray icon control we're operating on
        /// </summary>
        public TaskbarIcon TrayIcon { get; set; }

        public CaliburnMessageDispatcher(ILog logger, IEventAggregator aggregator)
        {
            _logger = logger;
            aggregator.Subscribe(this);
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

            var viewModel = IoC.Get<BrokenBuildViewModel>();
            var view = ViewLocator.LocateForModel(viewModel, null, null);
            viewModel.BuildMessage = "nah, it's bust again. Whatr are we going to do?! nk; sdfgl;kn dsflk;n sdfl;khj dsfl;k\r\nStuff on a new line";
            viewModel.Heading = "CI Build broken";            
            ViewModelBinder.Bind(viewModel, view, null);

            TrayAction(x => x.ShowCustomBalloon(view, PopupAnimation.Slide, 5000));
        }
    }
}