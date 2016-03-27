using System;
using Caliburn.Micro;
using SevanConsulting.Bugle.Helpers;

namespace SevanConsulting.Bugle.Services
{
    /// <summary>
    /// Manage configuration objects
    /// </summary>
    public class ConfigurationManager
    {

        private IEventAggregator _eventAggregator;

        public ConfigurationManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // hardcode for testing
            TfsDefaultCollectionUrl = new Uri("https://companyname.visualstudio.com/DefaultCollection");
        }

        /// <summary>
        /// Uri of default collection on a TFS server, eg https://company.visualstudio.com/defaultcollection/
        /// </summary>
        public Uri TfsDefaultCollectionUrl { get; set; }

        public void SetUserDetails(UserDetails user)
        {
            
        }
    }
}