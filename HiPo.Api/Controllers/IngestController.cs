using HiPo.Service.Models;
using System.Web.Mvc;

namespace HiPo.Api.Controllers
{
    public class IngestController : BaseController
    {
        // GET: Ingest

        [HttpPost]
        public JsonResult Index(string id)
        {
            return Json(new JsonResponse { id = id, message = "test index action [HTTP POST]", success = true });
        }


        [HttpPost]
        public JsonResult UploadString(XmlStringModel model)
        {
            var _result = BlobService.SaveBlobFromString(model.xmlContent, model.contentType, model.containerName, model.fileName);
            return Json(_result);
        }
    }
}