using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Configuration;

namespace ProductsDLL
{
    public abstract class ConnectionRepository
    {

        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policyCache;
        private readonly string _connectionString;
        private readonly object ConfigurationManager;

        protected SqlConnection Connection { get; private set; }

        /// <summary>
        /// Agrego un objeto al cache
        /// </summary>
        /// <param name="_object"></param>
        protected internal void CacheAdd(string key, object _object)
        {
            if (_object != null)
            {
                _cache.Add(key, _object, _policyCache);
                
            }
        }

        protected internal void CacheClear(string key)
        {
            _cache.Remove(key);
        }

        protected internal List<T> CacheContent<T>(string key)
        {
            var list = (List<T>)_cache[key];

            return list;
        }

        protected internal T CacheContentOne<T>(string key)
        {
            var _object = (T)_cache[key];

            if (_object != null)
            {
                return _object;
            }
            else
            {
                return default(T);
            }

        }

        protected ConnectionRepository(string appSettingKey)
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[appSettingKey].ConnectionString;
            Connection = new SqlConnection(_connectionString);
            _policyCache = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(24) };
        }
    }
}
