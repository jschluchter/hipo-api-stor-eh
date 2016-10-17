using HiPo.Service.Interfaces.Storage;
using HiPo.Service.Models;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiPo.Service
{
    public class BlobService : IBlobService
    {

        #region setup
        private readonly CloudStorageAccount _account;

        public BlobService(string dataConnectionString)
        {
            _account = CloudStorageAccount.Parse(dataConnectionString);
        }
        #endregion setup

        public JsonResponse SaveBlobFromStream(byte[] input, string contentType, string containerName, string fileName)
        {
            var _response = new JsonResponse();
            try
            {

                var _client = _account.CreateCloudBlobClient();
                var _container = _client.GetContainerReference(containerName);

                _container.CreateIfNotExists();

                var _blockBlob =_container.GetBlockBlobReference(fileName);
                _blockBlob.Properties.ContentType = contentType;

                using (var ms = new MemoryStream(input))
                {
                   _blockBlob.UploadFromStream(ms);
                }

                dynamic _meta = new ExpandoObject();
                _meta.BlobUrl = _blockBlob.StorageUri.PrimaryUri.ToString();
                _response.meta = _meta;
                _response.success = true;

            }
            catch (Exception ex)
            {

                _response.message = ex.Message;
                _response.success = false;

                Trace.TraceError(ex.Message);

            }
            return _response;



        }

        public JsonResponse SaveBlobFromString(string input, string contentType, string containerName, string fileName)
        {
            var _response = new JsonResponse();
            try
            {

                var _client = _account.CreateCloudBlobClient();
                var _container = _client.GetContainerReference(containerName);

                _container.CreateIfNotExists();

                var _blockBlob = _container.GetBlockBlobReference(fileName);
                _blockBlob.Properties.ContentType = contentType;

                _blockBlob.UploadText(input, Encoding.UTF8);

                dynamic _meta = new ExpandoObject();
                _meta.BlobUrl = _blockBlob.StorageUri.PrimaryUri.ToString();
                _response.meta = _meta;
                _response.success = true;

            }
            catch (Exception ex)
            {

                _response.message = ex.Message;
                _response.success = false;

                Trace.TraceError(ex.Message);

            }
            return _response;



        }



    }
}
