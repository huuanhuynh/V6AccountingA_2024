using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Common.Utils;


namespace V6Soft.Common.Cache
{
    /// <summary>
    /// This is a static encapsulation of the Framework provided MemoryCache 
    /// to make it easier to use.
    /// <para/>Domain (or "region") functionality missing in default MemoryCache is provided.
    /// <para/>This is very useful when adding items with identical keys but belonging to different domains.
    /// <para/>Example: "Customer" with Id=1, and "Product" with Id=1
    /// </summary>
    public class V6MemoryCache
    {
        private const char KeySeparator = '_';

        public static V6MemoryCache CreateCache(string domainName)
        {
            return new V6MemoryCache(domainName);
        }

        private MemoryCache Cache
        {
            get { return MemoryCache.Default; }
        }

        private string m_DomainName;

        // -----------------------------------------------------------------------------------------------------------------------------
        // The default instance of the MemoryCache is used.
        // Memory usage can be configured in standard config file.
        // -----------------------------------------------------------------------------------------------------------------------------
        // cacheMemoryLimitMegabytes:   The amount of maximum memory size to be used. Specified in megabytes. 
        //                              The default is zero, which indicates that the MemoryCache instance manages its own memory
        //                              based on the amount of memory that is installed on the computer. 
        // physicalMemoryPercentage:    The percentage of physical memory that the cache can use. It is specified as an integer value from 1 to 100. 
        //                              The default is zero, which indicates that the MemoryCache instance manages its own memory 
        //                              based on the amount of memory that is installed on the computer. 
        // pollingInterval:             The time interval after which the cache implementation compares the current memory load with the 
        //                              absolute and percentage-based memory limits that are set for the cache instance.
        //                              The default is two minutes.
        // -----------------------------------------------------------------------------------------------------------------------------
        //  <configuration>
        //    <system.runtime.caching>
        //      <memoryCache>
        //        <namedCaches>
        //          <add name="default" cacheMemoryLimitMegabytes="0" physicalMemoryPercentage="0" pollingInterval="00:02:00" />
        //        </namedCaches>
        //      </memoryCache>
        //    </system.runtime.caching>
        //  </configuration>
        // -----------------------------------------------------------------------------------------------------------------------------

        private V6MemoryCache(string domainName)
        {
            m_DomainName = domainName;
        }

        /// <summary>
        /// Stores an object and lets it stay in cache until manually removed.
        /// </summary>
        public void SetPermanent<TKey, TItem>(TKey key, TItem data)
        {
            CacheItemPolicy policy = new CacheItemPolicy { };
            Set(key, data, policy);
        }

        /// <summary>
        /// Stores an object and lets it stay in cache x minutes from write.
        /// </summary>
        public void SetAbsolute<TKey, TItem>(TKey key, TItem data, double minutes)
        {
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(minutes) };
            Set(key, data, policy);
        }

        /// <summary>
        /// Stores an object and lets it stay in cache x minutes from last write or read.
        /// </summary>
        public void SetSliding<TKey, TItem>(TKey key, TItem data, double minutes)
        {
            CacheItemPolicy policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(minutes) };
            Set(key, data, policy);
        }
           
        /// <summary>
        /// Gets item from cache.
        /// </summary>
        public TItem Get<TKey, TItem>(TKey key)
        {
            return (TItem)Cache.Get(CombineKey(key));
        }

        /// <summary>
        /// Checks if item exists in cache.
        /// </summary>
        public bool Exists<TKey>(TKey key)
        {
            return Cache[CombineKey(key)] != null;
        }

        /// <summary>
        /// Removes item from cache.
        /// </summary>
        public void Remove<TKey>(TKey key)
        {
            Cache.Remove(CombineKey(key));
        }

        /// <summary>
        /// Removes all items belonging to specified type.
        /// </summary>
        /// <param name="type">NULL will fallback to default type</param>
        public void Clear<TKey>()
        {
            string typeName = typeof(TKey).FullName;
            IEnumerable<string> keys = Cache.Select(item => item.Key);
            foreach (var combinedKey in keys)
            {
                if (typeName.Equals(ParseType(combinedKey)))
                    Cache.Remove(combinedKey);
            }
        }



        #region Private Methods
        
        /// <summary>
        /// Stores an item and lets it stay in cache according to specified policy.
        /// </summary>
        private void Set<TKey, TItem>(TKey key, TItem data, CacheItemPolicy policy)
        {
            Guard.ArgumentNotNull(key, "key");

            Cache.Add(CombineKey(key), data, policy);
        }

        private string ParseDomain(string combinedKey)
        {
            return combinedKey.Split(KeySeparator)[0];
        }

        private string ParseType(string combinedKey)
        {
            return combinedKey.Split(KeySeparator)[1];
        }
                
        private string ParseKey(string combinedKey)
        {
            return combinedKey.Split(KeySeparator)[2];
        }

        /// <summary>
        /// Create a combined key from given values.
        /// The combined key is used when storing and retrieving from the inner MemoryCache instance.
        /// Example: Product_76
        /// </summary>
        /// <param name="key">Key within specified type</param>
        /// <param name="type">NULL will fallback to default type</param>
        private string CombineKey<TKey>(TKey key)
        {
            string typeName = typeof(TKey).FullName;
            return string.Format("{0}{1}{2}{1}{3}",
                m_DomainName, 
                KeySeparator,
                typeName,
                key.ToString());
        }

        #endregion

    }
}
