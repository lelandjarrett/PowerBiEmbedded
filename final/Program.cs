using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FileUploader
{
    public class Program
    {
        public string _connectionString = "DefaultEndpointsProtocol=https;AccountName=customerimg;AccountKey=msy6Z07Ices57krBMawD5U0ArPnZzGRDBS92qzrrIqd5RJg5zC+n5dk7bqh6fcgb97g++lS3T8TQ+ASt69TOag==;EndpointSuffix=core.windows.net";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
