using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using StructureMap;

namespace SevanConsulting.Bugle.DI
{
    /// <summary>
    /// Caliburn Micro WPF bootstrapper that uses StructureMap for DI
    /// </summary>
    public class StructureMapBootstrapper: BootstrapperBase
    {
        protected IContainer RootContainer;
        public StructureMapBootstrapper()
        {
            Initialize();            
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            var rootRegistry = new Registry();
            rootRegistry.IncludeRegistry<DefaultRegistry>();

            RootContainer = new Container(rootRegistry);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            if (instance == null) return;
            RootContainer.BuildUp(instance);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param><param name="key">The key to locate.</param>
        /// <returns>
        /// The located service.
        /// </returns>
        protected override object GetInstance(Type service, string key)
        {
            object instance = null;
            if (service != null)
            {
                if (key == null)
                {
                    instance = RootContainer.GetInstance(service);
                }
                else
                {
                    instance = RootContainer.GetInstance(service, key);
                }                
            }
            if (instance == null)
            {
                throw new ApplicationException($"Cannot locate service {service?.FullName}, {key}");               
            }

            return instance;
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>
        /// The located services.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return RootContainer.GetAllInstances(service).OfType<object>();

        }
    }
}