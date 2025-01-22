using System.ComponentModel.DataAnnotations;

namespace ImageEventProcessor.Entities;

/// <summary>
/// Represents a post event containing information about an image.
/// </summary>
public class ImageData
{
    /// <summary>
    /// The URL of the image.
    /// </summary>
    [Required]
    [Url]
    public string ImageUrl { get; set; }

    /// <summary>
    /// The description of the image.
    /// </summary>
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageData"/> class with the specified image URL and description.
    /// </summary>
    /// <param name="imageUrl">The URL of the image.</param>
    /// <param name="description">The description of the image.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="imageUrl"/> or <paramref name="description"/> is null or empty.</exception>
    public ImageData(string imageUrl, string description)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));
        }

        ImageUrl = imageUrl;
        Description = description;
    }

    /// <summary>
    /// Empty constructor for serialization.
    /// </summary>
    public ImageData() { }
}
