using Caliburn.Micro;
using StructureMap;

namespace SevanConsulting.Bugle.DI
{
    /// <summary>
    /// Structuremap configuration.
    /// </summary>
    /// <remarks>
    /// StructureMap creates unknown classes on demand with a transient lifecycle. We only
    /// need to set up interface based classes or custom lifetimes
    /// </remarks>
    public class DefaultRegistry: Registry
    {
        public DefaultRegistry()
        {            
            For<IWindowManager>().Use(new WindowManager()).Singleton();
            For<ConfigurationManager>().Use(new ConfigurationManager()).Singleton();
            For<ILog>().Use(x => LogManager.GetLog(x.ParentType));
        }
    }
}