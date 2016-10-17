using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiPo.Service.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            success = true;
        }

        public string id { get; set; }
        public dynamic meta { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }
}
