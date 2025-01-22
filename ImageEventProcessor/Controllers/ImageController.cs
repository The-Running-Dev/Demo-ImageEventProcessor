using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

using ImageEventProcessor.Entities;
using Timer = System.Threading.Timer;

namespace ImageEventProcessor.Controllers
{
    /// <summary>
    /// Controller to handle image events.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private static readonly List<ImageDataExtended> Images = new();

        private static int HourlyCount = 0;

        private static Timer _resetTimer;

        static ImageController()
        {
            _resetTimer = new Timer(ResetEventCount, null, 3600000, 3600000); // 1 hour in milliseconds
        }

        private static void ResetEventCount(object? state)
        {
            HourlyCount = 0;
        }

        /// <summary>
        /// Posts a new image event.
        /// </summary>
        /// <param name="data">The image data to post.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost()]
        [SwaggerRequestExample(typeof(ApiResponse), typeof(ImageEventExample))]
        public IActionResult PostEvent([FromBody] ImageData data)
        {
            HourlyCount++;

            Images.Add(new ImageDataExtended(data.ImageUrl, data.Description, DateTime.UtcNow));

            return Ok(new ApiResponse(Messages.ImagePosted));
        }

        /// <summary>
        /// Gets the last posted image event.
        /// </summary>
        /// <returns>The last posted image event.</returns>
        [HttpGet("last-image")]
        public IActionResult GetLastImage()
        {
            if (Images.Count > 0)
            {
                var image = Images.FirstOrDefault();
                Images.RemoveAt(0);

                return Ok(new ApiResponse(image)
                {
                    HourlyCount = HourlyCount
                });
            }

            return NotFound(new ApiResponse(Messages.NoImageFound)
            {
                HourlyCount = HourlyCount
            });
        }

        /// Helper methods for testing

        /// <summary>
        /// Gets the number of events that have occurred in the last hour.
        /// </summary>
        /// <returns></returns>
        public static int GetHourlyCount()
        {
            return HourlyCount;
        }

        public static void ResetEventCountPublic()
        {
            HourlyCount = 0;
        }

        public static object GetImages()
        {
            return Images;
        }
    }
}