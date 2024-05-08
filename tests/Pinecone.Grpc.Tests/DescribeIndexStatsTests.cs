using FluentAssertions;
using Xunit;

namespace Pinecone.Grpc.Tests;

[Collection("Pinecone")]
public class DescribeIndexStatsTests(PineconeFixture fixture)
{
	private readonly VectorService.VectorServiceClient? _client = fixture.VectorServiceClient;

	[Fact]
	public void CanGetIndexStats()
	{
		var describeIndexStatsResponse = _client!.DescribeIndexStats(new DescribeIndexStatsRequest());
		describeIndexStatsResponse.Dimension.Should().BeGreaterThan(0);
	}

	[Fact]
	public async Task CanGetIndexStatsAsync()
	{
		var describeIndexStatsResponse = await _client!.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
		describeIndexStatsResponse.Dimension.Should().BeGreaterThan(0);
	}
}
