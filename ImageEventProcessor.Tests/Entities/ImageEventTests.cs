using NUnit.Framework;
using FluentAssertions;

using ImageEventProcessor.Entities;

namespace ImageEventProcessor.Tests.Entities;

[TestFixture]
public class ImageEventTests
{
    [Test]
    public void Constructor_ShouldInitializeProperties_WhenValidArguments()
    {
        var imageUrl = "https://example.com/image.jpg";
        var description = "This is a valid description.";

        var data = new ImageData(imageUrl, description);

        data.ImageUrl.Should().Be(imageUrl);
        data.Description.Should().Be(description);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Constructor_ShouldThrowArgumentException_WhenImageUrlIsInvalid(string invalidImageUrl)
    {
        var description = "This is a valid description.";

        var act = () =>
        {
            var data = new ImageData(invalidImageUrl, description);
        };

        act.Should().Throw<ArgumentException>()
            .WithMessage("Image URL cannot be null or empty. (Parameter 'imageUrl')");
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Constructor_ShouldThrowArgumentException_WhenDescriptionIsInvalid(string invalidDescription)
    {
        var imageUrl = "https://example.com/image.jpg";

        var act = () =>
        {
            var data = new ImageData(imageUrl, invalidDescription);
        };

        act.Should().Throw<ArgumentException>()
            .WithMessage("Description cannot be null or empty. (Parameter 'description')");
    }

    [Test]
    public void ParameterlessConstructor_ShouldInitializePropertiesToDefaultValues()
    {
        var imageEvent = new ImageData();

        imageEvent.ImageUrl.Should().BeNull();
        imageEvent.Description.Should().BeNull();
    }
}