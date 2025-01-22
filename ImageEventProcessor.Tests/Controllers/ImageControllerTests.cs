using NUnit.Framework;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using ImageEventProcessor.Entities;
using ImageEventProcessor.Controllers;

namespace ImageEventProcessor.Tests.Controllers;

[TestFixture]
public class ImageControllerTests
{
    private ImageController _controller;

    [SetUp]
    public void SetUp()
    {
        _controller = new ImageController();
    }

    [Test]
    public void PostEvent_ShouldReturnOk_WhenValidImageEvent()
    {
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");

        var result = _controller.PostEvent(data) as OkObjectResult;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);

        var response = result.Value as ApiResponse;

        response?.Message.Should().Be(Messages.ImagePosted);
    }

    [Test]
    public void GetLastImage_ShouldReturnOkWithLastImage_WhenImageEventPosted()
    {
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");
        _controller.PostEvent(data);

        var result = _controller.GetLastImage() as OkObjectResult;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);

        var returnedImageEvent = result.Value as ApiResponse;

        returnedImageEvent.Should().NotBeNull();
        returnedImageEvent.Image.ImageUrl.Should().Be(data.ImageUrl);
        returnedImageEvent.Image.Description.Should().Be(data.Description);
    }

    [Test]
    public void GetLastImage_ShouldReturnNotFound_WhenNoImageEventPosted()
    {
        var result = _controller.GetLastImage() as NotFoundObjectResult;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(404);

        var response = result.Value as ApiResponse;

        (response?.Message).Should().Be(Messages.NoImageFound);
    }

    [Test]
    public void ResetEventCount_ShouldResetHourlyCount()
    {
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");
        _controller.PostEvent(data);

        ImageController.ResetEventCountPublic();

        ImageController.GetHourlyCount().Should().Be(0);
    }

    [Test]
    public void PostEvent_ShouldIncrementHourlyCount()
    {
        var initialCount = ImageController.GetHourlyCount();
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");

        _controller.PostEvent(data);

        ImageController.GetHourlyCount().Should().Be(initialCount + 1);
    }

    [Test]
    public void GetLastImage_ShouldReturnCorrectHourlyCount()
    {
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");
        _controller.PostEvent(data);

        var result = _controller.GetLastImage() as OkObjectResult;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);

        var response = result.Value as ApiResponse;

        response?.HourlyCount.Should().BeGreaterThan(0);
    }

    // ...

    [Test]
    public void PostEvent_ShouldAddImageToList()
    {
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");

        _controller.PostEvent(data);

        var images = ImageController.GetImages() as IEnumerable<ImageData>;
        images.Should().NotBeNull();
        images.Should().HaveCount(1);
        var addedImage = images.First();
        addedImage.ImageUrl.Should().Be(data.ImageUrl);
        addedImage.Description.Should().Be(data.Description);
    }

    // ...

    [Test]
    public void GetLastImage_ShouldRemoveImageFromList()
    {
        var data = new ImageData("https://example.com/image.jpg", "This is a valid description.");
        _controller.PostEvent(data);

        _controller.GetLastImage();

        var images = ImageController.GetImages() as IEnumerable<ImageData>;
        images.Should().NotBeNull();
        images.Should().BeEmpty();
    }
}
