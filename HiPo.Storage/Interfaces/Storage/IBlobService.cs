using HiPo.Service.Models;

namespace HiPo.Service.Interfaces.Storage
{
    public interface IBlobService
    {
        JsonResponse SaveBlobFromStream(byte[] input, string contentType, string containerName, string fileName);
        JsonResponse SaveBlobFromString(string input, string contentType, string containerName, string fileName);
    }
}
