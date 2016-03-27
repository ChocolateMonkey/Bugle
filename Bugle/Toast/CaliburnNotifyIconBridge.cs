using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;

namespace SevanConsulting.Bugle.Toast
{
    /// <summary>
    /// Bridge Caliburn Micro to the NotifyIcon control via attached behaviour
    /// </summary>
    public static class CaliburnNotifyIconBridge
    {
    
        public static readonly DependencyProperty AttachedProperty =
            DependencyProperty.RegisterAttached("Attached", typeof (bool), typeof (CaliburnNotifyIconBridge),
                new PropertyMetadata(AttachChanged));

        private static CaliburnTrayBroker _viewBroker;


        public static bool GetAttached(TaskbarIcon tb)
        {
            return (bool)tb.GetValue(AttachedProperty);
        }

        /// <summary>
        /// Set to True to wire up tray icon to caliburn
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="value"></param>
        public static void SetAttached(TaskbarIcon tb, bool value)
        {
            tb.SetValue(AttachedProperty, value);
        }

        private static void AttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;
            var icon = d as TaskbarIcon;
            if (d == null) return;

            if ((bool)e.NewValue)
            {
                _viewBroker = IoC.Get<CaliburnTrayBroker>();
                _viewBroker.TrayIcon = icon;
            }
            else
            {
                _viewBroker.TrayIcon = null;
                _viewBroker = null;
            }
        }
    }
}