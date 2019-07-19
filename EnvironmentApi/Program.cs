using Nancy.Scaffolding;
using Nancy.Scaffolding.Models;

namespace EnvironmentApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ApiBasicConfiguration
            {
                ApiName = "Environment API - ",
                ApiPort = 80,
                EnvironmentVariablesPrefix = ""
            };

            Api.Run(config);
        }
    }
}
