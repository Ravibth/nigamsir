using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.AzureServices
{
    public interface IAzureBlobStorageService
    {
        string ReadBlobStorageFileContent(string containerName, string fileileName, ILogger log);
    }
}
