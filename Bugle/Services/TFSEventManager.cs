using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using Caliburn.Micro;

namespace SevanConsulting.Bugle.Services
{
    /// <summary>
    /// Service to manage communicating with TFS and handling the events we're interested in
    /// </summary>
    public class TfsEventManager
    {
        private readonly IEventAggregator _eventAggregator;

    
        public TfsEventManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.PublishOnUIThread("TestMessage");
        }

    }
}
