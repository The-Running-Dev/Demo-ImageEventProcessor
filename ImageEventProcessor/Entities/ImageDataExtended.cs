namespace ImageEventProcessor.Entities;

/// <summary>
/// Represents a post event containing information about an image.
/// </summary>
public class ImageDataExtended: ImageData
{
    /// <summary>
    /// The time the image was received.
    /// </summary>
    public DateTime? TimeStamp { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageData"/> class with the specified image URL and description.
    /// </summary>
    /// <param name="imageUrl">The URL of the image.</param>
    /// <param name="description">The description of the image.</param>
    /// <param name="timeStamp"></param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="imageUrl"/> or <paramref name="description"/> is null or empty.</exception>
    public ImageDataExtended(string imageUrl, string description, DateTime timeStamp) : base(imageUrl, description)
    {
        ImageUrl = imageUrl;
        Description = description;
        TimeStamp = timeStamp;
    }
}
