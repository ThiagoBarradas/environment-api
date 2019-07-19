using Nancy;
using Nancy.Scaffolding;
using Nancy.Scaffolding.Modules;
using System;
using System.Collections.Generic;
using WebApi.Models.Response;

namespace EnvironmentApi.Controllers
{
    public class EnvironmentController : BaseModule
    {
        public EnvironmentController()
        {
            this.Get("", args => this.Home());
            this.Get("environment/{envNames}", args => this.GetEnv(args.envNames));
        }

        public object Home()
        {
            return Response.AsText(Api.ApiBasicConfiguration.ApiName);
        }

        public object GetEnv(string envNames)
        {
            var envNamesArray = envNames?.Split('|') ?? new string[] { };
            var defaultValue = "[not found]";
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(var envName in envNamesArray)
            {
                try
                {
                    result[envName] = Environment.GetEnvironmentVariable(envName)
                        ?? defaultValue;
                }
                catch(Exception)
                {
                    result[envName] = defaultValue;
                }
            }

            return this.CreateJsonResponse(new ApiResponse
            {
                Content = result,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
