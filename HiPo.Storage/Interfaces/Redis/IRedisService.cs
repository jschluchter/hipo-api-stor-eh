using HiPo.Service.Models;

namespace HiPo.Service.Interfaces.Redis
{
    public interface IRedisService
    {
        JsonResponse AddToCache(string key, string value);
        JsonResponse GetFromCache(string key);

    }
}
