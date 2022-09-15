using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence;
using Core.Entities;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;
using Azure;
using Azure.Storage.Blobs;
using System;
using System.IO;

namespace FunctionApp3
{
    public class Function1
    {
        private readonly IConfiguration configuration;
        public Function1(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [FunctionName("Function1")]
        public async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            await context.CallActivityAsync<string>("Function1_Hello", "Tokyo");
            var file = await context.CallActivityAsync<string>("Download_File", "Seattle");



            return new string[] { "Hello Tokyo!", "Hello Seattle!", "Hello London!" }.ToList();
            //return file;
        }

        [FunctionName("Function1_Hello")]
        public async Task<string> SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {configuration["AzureKerVaultUrl"]}.");

            using (var context = new AppDbContext(configuration))
            {
                context.Author.Add(new Author { Name = name });
                context.SaveChanges();
                var mno = context.Author.Where(x => x.Name == name + System.DateTime.UtcNow.ToString()).ToList();
                if(mno != null)
                    log.LogInformation($"S JSON {JsonConvert.SerializeObject(mno)}.");
            }

            return $"Hello {configuration["AzureKerVaultUrl"]}!";
        }

        [FunctionName("Download_File")]
        public async Task<byte[]> DownloadFile([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Download File.");

            var sasCredential = new AzureSasCredential(configuration["Blob:SASToken"]);
            var uri = new Uri($"{configuration["Blob:SASUrl"]}{configuration["Blob:Path"]}/{"test.txt"}");
            var client = new BlobClient(uri, sasCredential);
            var memoryStream = new MemoryStream();
            await client.DownloadToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        [FunctionName("Function1_HttpStart")]
        public async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
