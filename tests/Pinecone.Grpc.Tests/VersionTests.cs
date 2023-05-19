using System.Configuration;
using System.Xml.Linq;
using FluentAssertions;
using Xunit;
using static Pinecone.Grpc.VectorService;

namespace Pinecone.Grpc.Tests;

public class VersionTests
{
	[Fact]
	public void VersionMatchesBuildVersion()
	{
		var buildXml = XDocument.Load(Path.Combine(SolutionPaths.Root, "Directory.Build.props"));
		var version = buildXml.Descendants(XName.Get("PineconeVersion", "http://schemas.microsoft.com/developer/msbuild/2003")).First().Value;
		VectorServiceClient.PineconeVersion.Should().Be(version);
	}
}
