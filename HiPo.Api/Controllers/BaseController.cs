using HiPo.Api.Code;
using HiPo.Service;
using HiPo.Service.Interfaces.EventHub;
using HiPo.Service.Interfaces.Redis;
using HiPo.Service.Interfaces.Storage;
using System.Web.Mvc;

namespace HiPo.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly IBlobService BlobService = new BlobService(AppConfig.PrimaryStorage);
        public readonly IRedisService RedisService = new RedisService(AppConfig.RedisConnection);
        public readonly IEventHubService EventHubService = new EventHubService(AppConfig.EventHubConnection, AppConfig.EventHubName);
    }
}