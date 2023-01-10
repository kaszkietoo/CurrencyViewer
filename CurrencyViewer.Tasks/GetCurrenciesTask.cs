using CurrencyViewer.Tasks.Models;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyViewer.Tasks
{
    public class GetCurrenciesTask
    {
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        private static string DatabaseId = "CurrencyViewer";
        private static string ContainerId = "Currencies";
        private static string CurrenciesUrl = @"https://openexchangerates.org/api/currencies.json";


        public static async Task Execute()
        {
            var cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions { ApplicationName = "CurrencyViewer" });
            var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
            var exisitingContainer = database.Database.GetContainer(ContainerId);
            await exisitingContainer.DeleteContainerAsync();
            var container = await database.Database.CreateContainerIfNotExistsAsync(ContainerId, "/partitionKey");      
            

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(CurrenciesUrl);
            var json = await response.Content.ReadAsStringAsync();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);      

            foreach (var pair in dictionary)
            {         
                await container.Container.CreateItemAsync(new Currency { Code = pair.Key, Name = pair.Value }, new PartitionKey(pair.Value));                
            }

            var feed = container.Container.GetItemQueryIterator<Currency>( queryText: $"SELECT * FROM {ContainerId}" );
            while (feed.HasMoreResults)
            {

            }
        }
    }
}
