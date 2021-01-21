using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UselessFacts
{
    public static class FetchPhrase
    {
        [FunctionName("FetchPhrase")]
        public static async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string data = String.Empty;

            List<Phrase> phrase = await GetPhrase(log);

            try
            {
                data = JsonConvert.SerializeObject(phrase);
            }
            catch (Exception ex)
            {
                log.LogError($"Error when trying to serialize object to JSON: {ex}");
            }

            return data != "" ? data : "Couldn't fetch data from database";
        }

        [FunctionName(Strings.GetPhrase)]
        private static async Task<List<Phrase>> GetPhrase(ILogger log)
        {
            log.LogInformation($"Executed 'GetPhrase()'");

            List<Phrase> phrase = new List<Phrase>();

            try
            {
                phrase = await DBConnector.GetPhrase(ConfigReader.ConnectionString);
            }
            catch (Exception ex)
            {
                log.LogInformation($"Exception when trying to connect to database: {ex}");
            }

            return phrase;
        }
    }
}
