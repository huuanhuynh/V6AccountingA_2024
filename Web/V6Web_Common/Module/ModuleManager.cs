using System.Collections.Generic;


namespace V6Soft.Web.Common.Module
{
    /// <summary>
    ///     Provides a hub where all modules register to it.
    /// </summary>
    public class ModuleManager
    {
        #region Static

        private static ModuleManager s_Instance;

        /// <summary>
        ///     Gets singleton instance of ModuleManager
        /// </summary>
        public static ModuleManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new ModuleManager();
                }
                return s_Instance;
            }
        }

        #endregion

        /// <summary>
        ///     Gets or sets collection of managed modules.
        /// </summary>
        public IEnumerable<WebModule> Modules { get; set; }

        private ModuleManager()
        {

        }
    }
}
