using HiPo.Service.Interfaces.EventHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiPo.Service.Models;
using Microsoft.ServiceBus.Messaging;
using System.Dynamic;

namespace HiPo.Service
{
    public class EventHubService : IEventHubService
    {
        private static EventHubClient _eventHubClient;
        public EventHubService(string connection, string hubname) {

            _eventHubClient = EventHubClient.CreateFromConnectionString(connection, hubname);

        }

        public JsonResponse AddToEventHub(string data)
        {

            var _response = new JsonResponse();
            dynamic _meta = new ExpandoObject();
            try
            {
                _eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(data)));
                _response.success = true;
            }
            catch (Exception ex)
            {

                _meta.errorMessage = ex.Message;
                _response.success = false;
            }
            _response.meta = _meta;
            return _response;
        }
    }
}
