using HiPo.Api.Code;
using HiPo.Service;
using HiPo.Service.Interfaces.Storage;
using System.Web.Mvc;

namespace HiPo.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly IBlobService BlobService = new BlobService(AppConfig.PrimaryStorage);
    }
}