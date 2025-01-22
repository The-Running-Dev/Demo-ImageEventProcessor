namespace ImageEventProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (string.Equals(Environment.GetEnvironmentVariable("AWS_EXECUTION_ENV"), "AWS_Lambda_dotnetcore", StringComparison.OrdinalIgnoreCase))
            {
                var lambdaEntry = new LambdaEntryPoint();

                lambdaEntry.FunctionHandlerAsync(null, null).GetAwaiter().GetResult();
            }
            else
            {
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}