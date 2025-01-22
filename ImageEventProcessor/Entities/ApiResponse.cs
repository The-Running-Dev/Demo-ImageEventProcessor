namespace ImageEventProcessor.Entities;

/// <summary>
/// Represents an event containing information about an image.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// The extended image data associated with the event.
    /// </summary>
    public ImageDataExtended Image { get; set; }

    /// <summary>
    /// The number of events that have occurred in the last hour.
    /// </summary>
    public int HourlyCount { get; set; }

    /// <summary>
    /// Gets the message associated with the event result.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse"/> class with the specified image postData.
    /// </summary>
    /// <param name="data">The image postData</param>
    public ApiResponse(ImageDataExtended data)
    {
        Image = data;
        Message = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public ApiResponse(string message)
    {
        Message = message;
    }
}
