using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Pinecone.Grpc;

/// <summary>
/// Abstraction of client-side RPC invocation for interacting with Pinecone service
/// </summary>
public class PineconeCallInvoker : CallInvoker
{
	private readonly CallInvoker _callInvoker;

	/// <summary>
	/// Instantiates a new instance of <see cref="PineconeCallInvoker"/>
	/// </summary>
	/// <param name="channel">the channel to connect to the Pinecone service</param>
	/// <param name="apiKey">the API key to send with each request</param>
	public PineconeCallInvoker(GrpcChannel channel, string apiKey) =>
		_callInvoker = channel.Intercept(metadata =>
		{
			metadata.Add("api-key", apiKey);
			return metadata;
		});

	/// <inheritdoc />
	public override TResponse BlockingUnaryCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string? host, CallOptions options, TRequest request) => _callInvoker.BlockingUnaryCall(method, host, options, request);

	/// <inheritdoc />
	public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string? host, CallOptions options, TRequest request) => _callInvoker.AsyncUnaryCall(method, host, options, request);

	/// <inheritdoc />
	public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string? host, CallOptions options,
		TRequest request) =>
		_callInvoker.AsyncServerStreamingCall(method, host, options, request);

	/// <inheritdoc />
	public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string? host, CallOptions options) => _callInvoker.AsyncClientStreamingCall(method, host, options);

	/// <inheritdoc />
	public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string? host, CallOptions options) => _callInvoker.AsyncDuplexStreamingCall(method, host, options);
}
