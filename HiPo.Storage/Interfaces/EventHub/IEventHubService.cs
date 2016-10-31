using HiPo.Service.Models;

namespace HiPo.Service.Interfaces.EventHub
{
    public interface IEventHubService
    {
        JsonResponse AddToEventHub(string data);
    }
}
