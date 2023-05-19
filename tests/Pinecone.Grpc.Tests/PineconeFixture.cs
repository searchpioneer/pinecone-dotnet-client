using System.Text.Json;
using Grpc.Core;
using Xunit;

namespace Pinecone.Grpc.Tests;

[CollectionDefinition("Pinecone")]
public sealed class PineconeCollection : ICollectionFixture<PineconeFixture> { }

public sealed class PineconeFixture : IAsyncLifetime
{
	private PineconeChannel _channel = null!;
	private const string PineconeProjectname = "PINECONE_PROJECTNAME";
	private const string PineconeIndexname = "PINECONE_INDEXNAME";
	private const string PineconeEnvironment = "PINECONE_ENVIRONMENT";
	private const string PineconeApikey = "PINECONE_APIKEY";

	private const string ProjectName = "projectName";
	private const string IndexName = "indexName";
	private const string Environment = "environment";
	private const string ApiKey = "apiKey";

	public ClientConfiguration? Configuration { get; private set; }

	public VectorService.VectorServiceClient? VectorServiceClient { get; private set; }

    public Task InitializeAsync()
    {
	    string? projectName;
	    string? indexName;
	    string? environment;
	    string? apiKey;

	    var credentials = Path.Combine(SolutionPaths.Root, "credentials.json");
	    if (File.Exists(credentials))
	    {
		    var document = JsonDocument.Parse(File.ReadAllText(credentials));
		    if (!document.RootElement.TryGetProperty(ProjectName, out var projectNameElement))
			    ThrowCredentialsValueMissing(ProjectName);

		    projectName = projectNameElement.GetString();
		    if (string.IsNullOrEmpty(projectName))
			    ThrowCredentialsValueEmpty(ProjectName);

		    if (!document.RootElement.TryGetProperty(IndexName, out var indexNameElement))
			    ThrowCredentialsValueMissing(IndexName);

		    indexName = indexNameElement.GetString();
		    if (string.IsNullOrEmpty(indexName))
			    ThrowCredentialsValueEmpty(IndexName);

		    if (!document.RootElement.TryGetProperty(Environment, out var environmentElement))
			    ThrowCredentialsValueMissing(Environment);

		    environment = environmentElement.GetString();
		    if (string.IsNullOrEmpty(environment))
			    ThrowCredentialsValueEmpty(Environment);

		    if (!document.RootElement.TryGetProperty(ApiKey, out var apiKeyElement))
			    ThrowCredentialsValueMissing(ApiKey);

		    apiKey = apiKeyElement.GetString();
		    if (string.IsNullOrEmpty(apiKey))
			    ThrowCredentialsValueEmpty(ApiKey);
	    }
	    else
	    {
		    projectName = System.Environment.GetEnvironmentVariable(PineconeProjectname);
		    if (string.IsNullOrEmpty(projectName))
			    ThrowEnvironmentVariableMissing(PineconeProjectname);

		    indexName = System.Environment.GetEnvironmentVariable(PineconeIndexname);
		    if (string.IsNullOrEmpty(indexName))
			    ThrowEnvironmentVariableMissing(PineconeIndexname);

		    environment = System.Environment.GetEnvironmentVariable(PineconeEnvironment);
		    if (string.IsNullOrEmpty(environment))
			    ThrowEnvironmentVariableMissing(PineconeEnvironment);

		    apiKey = System.Environment.GetEnvironmentVariable(PineconeApikey);
		    if (string.IsNullOrEmpty(apiKey))
			    ThrowEnvironmentVariableMissing(PineconeApikey);
	    }

	    _channel = PineconeChannel.ForConfiguration(new ClientConfiguration(environment!, projectName!, apiKey!), indexName!);
	    VectorServiceClient = new VectorService.VectorServiceClient(_channel);

	    return Task.CompletedTask;
    }

    private void ThrowCredentialsValueMissing(string name) =>
	    throw new Exception($"{name} property is missing from credentials.json");
    private void ThrowCredentialsValueEmpty(string name) =>
	    throw new Exception($"{name} property is empty in credentials.json");

    private void ThrowEnvironmentVariableMissing(string name) =>
	    throw new Exception($"{name} environment variable is missing");

    public Task DisposeAsync()
    {
	    _channel!.Dispose();
	    return Task.CompletedTask;
    }
}
