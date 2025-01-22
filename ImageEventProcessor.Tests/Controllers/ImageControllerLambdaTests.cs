//using System.Text.Json;

//using NUnit.Framework;
//using FluentAssertions;

//using Amazon.Lambda.TestUtilities;
//using Amazon.Lambda.APIGatewayEvents;

//namespace ImageEventProcessor.Tests.Controllers;

//[TestFixture]
//public class ImageControllerLambdaTests
//{
//    [Test]
//    public async Task SimulateRequest()
//    {
//        var lambdaFunction = new LambdaEntryPoint();
//        var request = new APIGatewayProxyRequest
//        {
//            HttpMethod = "POST",
//            Path = "/api/image",
//            Body = "{\"ImageUrl\": \"http://example.com\", \"Description\": \"A sample image\"}",
//            Headers = new Dictionary<string, string>
//                {
//                    { "Content-Type", "application/json" }
//                },
//        };

//        var context = new TestLambdaContext();
//        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

//        response.StatusCode.Should().Be(200);
//        response.Body.Should().Contain("Event received successfully.");
//    }

//    [Test]
//    public async Task GetLastImage_ShouldReturnOkWithLastImage_WhenImageEventPosted()
//    {
//        var lambdaFunction = new LambdaEntryPoint();
//        var requestStr = await File.ReadAllTextAsync("./SampleRequests/ImageController-Get.json");
//        var request = JsonSerializer.Deserialize<APIGatewayProxyRequest>(requestStr, new JsonSerializerOptions
//        {
//            PropertyNameCaseInsensitive = true
//        });
//        var context = new TestLambdaContext();
//        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

//        response.StatusCode.Should().Be(200);
//        response.Body.Should().Be("[\"https://example.com/image1.jpg\",\"https://example.com/image2.jpg\"]");
//        response.MultiValueHeaders.Should().ContainKey("Content-Type");
//        response.MultiValueHeaders["Content-Type"].Should().ContainSingle().Which.Should().Be("application/json; charset=utf-8");
//    }
//}