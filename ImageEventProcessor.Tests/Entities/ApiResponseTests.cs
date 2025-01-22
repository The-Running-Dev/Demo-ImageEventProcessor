using NUnit.Framework;
using FluentAssertions;

using ImageEventProcessor.Entities;

namespace ImageEventProcessor.Tests.Entities;

[TestFixture]
public class ApiResponseTests
{
    [Test]
    public void Constructor_ShouldInitializeMessageProperty()
    {
        var message = "Test message";
        var result = new ApiResponse(message);

        result.Message.Should().Be(message);
    }

    [Test]
    public void MessageProperty_ShouldBeReadOnly()
    {
        var message = "Test message";
        var result = new ApiResponse(message);

        var messageProperty = typeof(ApiResponse).GetProperty("Message");

        messageProperty.Should().NotBeNull();
        messageProperty.CanWrite.Should().BeTrue();
    }
}