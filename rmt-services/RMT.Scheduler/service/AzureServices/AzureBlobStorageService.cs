using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.AzureServices
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobClient;

        public AzureBlobStorageService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public string ReadBlobStorageFileContent(string containerName, string fileileName, ILogger log)
        {
            string fileContent = string.Empty;
            try
            {
                var containerClient = _blobClient.GetBlobContainerClient(containerName);
                var blobFileClient = containerClient.GetBlockBlobClient(fileileName);
                var contentStream = blobFileClient.DownloadAsync().Result;
                using (var streamReader = new StreamReader(contentStream.Value.Content))
                {
                    while (!streamReader.EndOfStream)
                    {
                        fileContent = streamReader.ReadToEnd();
                        log.LogInformation($"--ReadBlobStorageFileContent--{fileContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogInformation(ex, "BlobServiceClient--Exception--Message--{0}", ex.Message);
                throw;
            }

            return fileContent;
        }
    }
}
