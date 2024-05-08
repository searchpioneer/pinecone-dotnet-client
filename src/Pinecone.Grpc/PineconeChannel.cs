using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Pinecone.Grpc;

/// <summary>
/// A gRPC channel to Pinecone service.
/// Channels are an abstraction of long-lived connections to remote servers.
/// </summary>
public class PineconeChannel : ChannelBase, IDisposable
{
	private readonly GrpcChannel _channel;
	private readonly string _apiKey;

	/// <inheritdoc />
	private PineconeChannel(GrpcChannel channel, string apiKey) : base(channel.Target)
	{
		_channel = channel;
		_apiKey = apiKey;
	}

	/// <inheritdoc />
	public override CallInvoker CreateCallInvoker() => _channel.Intercept(metadata =>
	{
		metadata.Add("api-key", _apiKey);
		return metadata;
	});

	/// <summary>
	/// Creates a <see cref="PineconeChannel"/> for the specified configuration and index name
	/// </summary>
	/// <param name="configuration">the configuration to connect to Pinecone</param>
	/// <param name="indexName">the index name to connect to Pinecone</param>
	/// <returns></returns>
	public static PineconeChannel ForConfiguration(ClientConfiguration configuration, string indexName)
	{
		var channel = GrpcChannel.ForAddress(
			$"https://{indexName}-{configuration.ProjectName}.svc.{configuration.Environment}.pinecone.io");
		return new PineconeChannel(channel, configuration.ApiKey);
	}

	/// <inheritdoc />
	public void Dispose() => _channel.Dispose();
}
