using V6Soft.Common.Logging;
using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Services.Common.Infrastructure
{
    /// <summary>
    /// Provides global environment with most widely-used objects. Contents of this class is application-specific
    /// and can be accessed by all classes in application.
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// Gets instance of dependency injector. This property can only be set at application initialization.
        /// </summary>
        public static IDependencyInjector DependencyInjector { get; internal set; }

        /// <summary>
        /// Gets instance of logger. This property can only be set at application initialization.
        /// </summary>
        public static ILogger Logger { get; internal set; }
    }
}
