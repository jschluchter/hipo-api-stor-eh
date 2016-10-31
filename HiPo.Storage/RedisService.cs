using HiPo.Service.Interfaces.Redis;
using HiPo.Service.Models;
using StackExchange.Redis;
using System;
using System.Dynamic;

namespace HiPo.Service
{
    public class RedisService : IRedisService
    {

        #region "setup"

        private static Lazy<ConnectionMultiplexer> _lazyConnection;
        public RedisService(string connection)
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(connection);
            });
        }

        #endregion

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        public JsonResponse AddToCache(string key, string value) {

            var _response = new JsonResponse();
            dynamic _meta = new ExpandoObject();
            try
            {
                IDatabase cache = Connection.GetDatabase();
                _response.id = key;
                _response.success = cache.StringSet(key, value);
            }
            catch(Exception ex)
            {
                
                _meta.errorMessage = ex.Message;
                _response.success = false;
            }
            _response.meta = _meta;
            return _response;

        }

        public JsonResponse GetFromCache(string key)
        {
            var _response = new JsonResponse();
            dynamic _meta = new ExpandoObject();
            try
            {
                IDatabase cache = Connection.GetDatabase();

                _meta.cacheValue = cache.StringGet(key).ToString();
                _response.id = key;
                _response.success = true;
                
            }
            catch (Exception ex)
            {
                _meta.cacheValue = null;
                _meta.errorMessage = ex.Message;
                _response.success = false;
            }
            _response.meta = _meta;
            return _response;
        }
    
    }
}
