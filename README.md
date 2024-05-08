[<img align="right" width="250" height="auto" src="https://searchpioneer.com/assets/svg/logos/logo.svg">](https://searchpioneer.com/)

Learn more about our services and expertise: [https://searchpioneer.com/](https://searchpioneer.com/)

# Pinecone .NET client

A .NET gRPC client for [Pinecone vector database](https://www.pinecone.io/).

## Getting started

### Installing

```sh
dotnet add package Pinecone.Grpc --version 1.0.0-alpha1
```

### Usage

The `Pinecone.Grpc.VectorService` provides an entry point to interact with all of 
Pinecone's gRPC services

```csharp
using static Pinecone.Grpc.VectorService;

namespace Example;

public class Program
{
	public static void Main(string[] args)
	{
		var configuration = new ClientConfiguration(
		    "<environment>",
		    "<projectName>",
		    "<apiKey>");
		    
		var channel = PineconeChannel.ForConfiguration(configuration, "<indexName>");
		var client = new VectorServiceClient(channel);

		var describeIndexStatsResponse = 
		    client.DescribeIndexStats(new DescribeIndexStatsRequest());
	}
}
```
