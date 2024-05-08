namespace Pinecone.Grpc;

/// <summary>
/// Configuration to interact with Pinecone service
/// </summary>
public class ClientConfiguration
{
	/// <summary>
	/// Gets the environment
	/// </summary>
	public string Environment { get; }
	/// <summary>
	/// Gets the project name
	/// </summary>
	public string ProjectName { get; }
	/// <summary>
	/// Gets the API key
	/// </summary>
	public string ApiKey { get; }

	/// <summary>
	/// Instantiates a new instance of <see cref="ClientConfiguration"/> to interact with Pinecone
	/// </summary>
	/// <param name="environment">The environment e.g. eu-west1-gcp</param>
	/// <param name="projectName">The project name. e.g. f43c8f1</param>
	/// <param name="apiKey">The API key e.g.</param>
	public ClientConfiguration(string environment, string projectName, string apiKey)
	{
		Environment = environment;
		ProjectName = projectName;
		ApiKey = apiKey;
	}
}
