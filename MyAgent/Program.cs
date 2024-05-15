using Microsoft.SemanticKernel;
using Kernel = Microsoft.SemanticKernel.Kernel;
using System.Runtime;
using Microsoft.VisualBasic;

var builder = Kernel.CreateBuilder();

var (useAzureOpenAI, model, azureEndpoint, apiKey, orgId) = Settings.LoadFromFile();

builder.Services.AddAzureOpenAIChatCompletion(model, azureEndpoint, apiKey);