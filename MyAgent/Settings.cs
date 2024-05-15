using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyAgent
{
    internal class Settings
    {
        private const string DefaultConfigFile = "config/settings.json";
        private const string TypeKey = "type";
        private const string ModelKey = "model";
        private const string EndpointKey = "endpoint";
        private const string SecretKey = "apikey";
        private const string OrgKey = "org";

        public static (bool useAzureOpenAI, string model, string azureEndpoint, string apiKey, string orgId)
        LoadFromFile(string configFile = DefaultConfigFile)
        {
            if (!File.Exists(configFile))
            {
                Console.WriteLine("Configuration not found: " + configFile);
                Console.WriteLine("\nPlease run the Setup Notebook (0-AI-settings.ipynb) to configure your AI backend first.\n");
                throw new Exception("Configuration not found, please setup the notebooks first using notebook 0-AI-settings.pynb");
            }

            try
            {
                var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(configFile));
                bool useAzureOpenAI = config[TypeKey] == "azure";
                string model = config[ModelKey];
                string azureEndpoint = config[EndpointKey];
                string apiKey = config[SecretKey];
                string orgId = config[OrgKey];
                if (orgId == "none") { orgId = ""; }

                return (useAzureOpenAI, model, azureEndpoint, apiKey, orgId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: " + e.Message);
                return (true, "", "", "", "");
            }
        }
    }
}
