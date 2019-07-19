using Nancy.Scaffolding;
using Nancy.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;

namespace EnvironmentApi.Tests
{
    public class EnvironmentControllerTest
    {
        public EnvironmentControllerTest()
        {
            // setup env vars
            Environment.SetEnvironmentVariable("ENV_TEST_SINGLE", "VALUE_TEST_SINGLE");
            Environment.SetEnvironmentVariable("ENV_TEST_MULTI", "VALUE_TEST_MULTI");
            Environment.SetEnvironmentVariable("ENV_TEST_EMPTY", "");
            Environment.SetEnvironmentVariable("ENV_TEST_NULL", null);
        }

        [Theory]
        [InlineData("ENV_TEST_SINGLE", "VALUE_TEST_SINGLE")]
        [InlineData("ENV_TEST_SINGLE|ENV_TEST_MULTI", "VALUE_TEST_SINGLE|VALUE_TEST_MULTI")]
        [InlineData("ENV_TEST_SINGLE|ENV_TEST_EMPTY", "VALUE_TEST_SINGLE|[not found]")]
        [InlineData("ENV_TEST_SINGLE|ENV_TEST_NULL", "VALUE_TEST_SINGLE|[not found]")]
        [InlineData("ENV_TEST_NULL", "[not found]")]
        public void GetEnv_Should_Returns_Values_From_System_Env_Vars(string envVars, string expectedResults)
        {
            // arrange
            var browser = new Browser(new Bootstrapper());
            var endpoint = string.Format("/environment/{0}", HttpUtility.UrlEncode(envVars));
            var envVarsArray = envVars.Split("|");
            var expectedResultsArray = expectedResults.Split("|");

            // act 
            var response = browser.Get(endpoint, (with) => {
                with.HttpRequest();
            }).Result;

            // assert
            var json = response.Body.AsString();
            var responseDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            Assert.Equal(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(envVarsArray.Count(), responseDic.Count);
            
            for (var i = 0; i < envVarsArray.Count(); i++)
            {
                var key = envVarsArray[i];
                var value = expectedResultsArray[i];
                Assert.Equal(responseDic[key], value);
            }
        }
    }
}
