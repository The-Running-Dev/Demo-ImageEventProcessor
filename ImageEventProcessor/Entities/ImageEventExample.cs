using Swashbuckle.AspNetCore.Filters;

namespace ImageEventProcessor.Entities;

/// <summary>
/// Provides an example for the ImagePostData class.
/// </summary>
public class ImageEventExample : IExamplesProvider<ImageData>
{
    /// <summary>
    /// Returns a basic example of the ImagePostData class.
    /// </summary>
    /// <returns></returns>
    public ImageData GetExamples()
    {
        return new ImageData("https://example.com/image.jpg", "A sample image description.");
    }
}