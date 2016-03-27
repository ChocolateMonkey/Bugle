using System;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using SevanConsulting.Bugle.Helpers;

namespace SevanConsulting.Bugle.Services
{
    /// <summary>
    /// Service to manage communicating with TFS and handling the events we're interested in
    /// </summary>
    public class TfsEventManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ConfigurationManager _configManager;
        private readonly ILog _logger;
        private VssConnection _currentConnection;
        private BuildHttpClient _BuildClient;


        public TfsEventManager(IEventAggregator eventAggregator, ConfigurationManager configManager, ILog logger)
        {
            _eventAggregator = eventAggregator;
            _configManager = configManager;
            _logger = logger;
        }

        /// <summary>
        /// Connect to TFS - this may ask the user for credentials, depending on the status of the connection
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            /*
            eg: https://company.visualstudio.com/defaultcollection/_apis/projects?api-version=1.0

             // Create instance of VssConnection using AD Credentials for AD backed account
            VssConnection connection = new VssConnection(new Uri(collectionUri), new VssAadCredential());

            // VssCredentials - NTLM against an on-prem server
            VssConnection connection = new VssConnection(new Uri(tfsUri), new VssCredentials());
            
            // VssConnection - VS sign in prompt
            VssConnection connection = new VssConnection(new Uri(collectionUri), new VssClientCredentials());

            */

            var tfsUri = _configManager.TfsDefaultCollectionUrl;

            if (tfsUri == null) return;

            // Use VS client credentails, including default windows credential store for caching
            VssClientCredentialStorage storage = new VssClientCredentialStorage();
            var creds = new VssClientCredentials();
            creds.Storage = storage;

            // attempt to connect
            try
            {
                _currentConnection = new VssConnection(tfsUri, creds);
                await _currentConnection.ConnectAsync(VssConnectMode.Automatic);
            }
            catch (TaskCanceledException)
            {
                //todo - set some application state to reflect not logged in status
                return;
            }
            catch (VssServiceException)
            {
                // something bad happened - can't do a lot here...
                return;
            }

            if (_currentConnection.HasAuthenticated)
            {
                // update app with logged in credentials, if present.
                var details = new UserDetails();

                details.DisplayName = _currentConnection.AuthorizedIdentity?.DisplayName;
                string connectedAccount = null;
                if (_currentConnection.AuthorizedIdentity?.Properties?.TryGetValue("Account", out connectedAccount) ==
                    true)
                {
                    details.Account = connectedAccount;
                }
                _configManager.SetUserDetails(details);

                // get an REST wrapper for the build APIs
                _BuildClient = await _currentConnection.GetClientAsync<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();
            }
        }

        /// <summary>
        /// Start polling for build failures
        /// </summary>
        /// <returns></returns>
        public async Task StartMonitoring()
        {
            _logger.Info("Stsrted monitoring for events");
            // Build definitions
            //https://{instance}/defaultcollection/{project}/_apis/build/definitions?api-version={version}[&projectname={string}]
            if (_BuildClient == null) return;

            // build definitions to monitor results for
            var buildDefinitions = new[] {2, 3};

            // invoke against known project for test
            var projectGuid = new Guid("544fe1dc-b92a-4a8e-83f9-1a67819e3458");

            var brokenBuilds = await _BuildClient.GetBuildsAsync(projectGuid, buildDefinitions, statusFilter: BuildStatus.Completed,
                resultFilter: BuildResult.Failed);

            var firstBuild = brokenBuilds.FirstOrDefault();

            if (firstBuild != null)
            {
                var msg = new BrokenBuildMessage();
                msg.BuildDate = firstBuild.FinishTime;
                msg.BuildNumber = firstBuild.BuildNumber;
                msg.BuildDefinitionName = firstBuild.Definition.Name;
                _eventAggregator.PublishOnUIThread(msg);
            }


        }

    }
}
