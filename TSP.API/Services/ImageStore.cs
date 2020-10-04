using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TSPServer.Services
{
    public class ImageStore
    {
        private readonly IConfiguration configuration;
        CloudBlobClient blobClient;
        string baseUri = "https://tspdevapistorage.blob.core.windows.net/";

        public ImageStore(IConfiguration configuration)
        {
            var storageAccountName= configuration["StorageAccount"];
            baseUri = $"https://{storageAccountName}.blob.core.windows.net/";
            var credentials = new StorageCredentials(storageAccountName, configuration["StorageKey"]);
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
            this.configuration = configuration;
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
