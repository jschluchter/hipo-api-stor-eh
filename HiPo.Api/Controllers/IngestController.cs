using HiPo.Service;
using HiPo.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
        public JsonResult UploadData(XmlStringModel model)
        {
            var _result = new JsonResponse();
            dynamic _meta = new ExpandoObject();

            // give a sample id
            var _id = Guid.NewGuid().ToString().Substring(0,8);
            _result.id = _id;


            // save to blob
            var _blobResult = BlobService.SaveBlobFromString(model.xmlContent, model.contentType, model.containerName, _id + ".xml");

            // handle blobsave error
            if (!_blobResult.success) return Json(_blobResult);

            // add blob url to response
           _meta.BlobUrl = _blobResult.meta.BlobUrl;

            var _redisResult = RedisService.AddToCache(_id, model.xmlContent);

            //handle redis save error
            if (!_redisResult.success) return Json(_redisResult);

            // add redis success to response
            _meta.RedisSuccess = true;

            var _item = new KeyValuePair<string, string>(_id, model.xmlContent);
            var _eventHubResult = EventHubService.AddToEventHub(JsonConvert.SerializeObject(_item));


            _result.meta = _meta;

            return Json(_result);
        }

        [HttpGet]
        public JsonResult GetById(string id)
        {
            var _result = RedisService.GetFromCache(id);
            return Json(_result,JsonRequestBehavior.AllowGet);
        }

    }
}