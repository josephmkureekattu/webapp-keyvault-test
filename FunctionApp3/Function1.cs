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
            outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("Function1_Hello", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName("Function1_Hello")]
        public string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {configuration["AzureKerVaultUrl"]}.");
            using (var context = new AppDbContext(configuration))
            {
                context.Author.Add(new Author { Name = "dsytfgy342r53i" });
                context.SaveChanges();
                var mno = context.Author.Where(x => x.Name == "dsytfgy342r53i" + System.DateTime.UtcNow.ToString()).ToList();
                if(mno != null)
                    log.LogInformation($"S JSON {JsonConvert.SerializeObject(mno)}.");
            }

            return $"Hello {configuration["AzureKerVaultUrl"]}!";
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
