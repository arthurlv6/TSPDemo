using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TSPServer.Services
{
    public class ImageStore
    {
        CloudBlobClient blobClient;
        string baseUri = "https://tspdevapistorage.blob.core.windows.net/";

        public ImageStore()
        {
            var credentials = new StorageCredentials("tspdevapistorage", "DDhuo+im5EWwsTaBUTjU+692Q2xvKPS7ulXZ0WQRYUVukJbl5grISP/f1ZAB+A1iviHomC1myt/SS+Rbn5sgpA==");
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
        }

        public async Task<string> SaveImage(Stream imageStream)
        {
            var imageId = Guid.NewGuid().ToString();
            
            var container = blobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            await blob.UploadFromStreamAsync(imageStream);
            return imageId;
        }

        public string UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddDays(-10),
                SharedAccessExpiryTime = DateTime.MaxValue
            };

            var container = blobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{baseUri}images/{imageId}{sas}";
        }
    }
}
