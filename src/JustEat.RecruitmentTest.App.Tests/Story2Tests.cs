using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustEat.RecruitmentTest.App.Business;
using NUnit.Framework;

namespace JustEat.RecruitmentTest.App.Tests
{
    /// <summary>
    /// Story 2 - Querying the API
    ///As an API consumer
    ///I want to query the restaurant API
    ///So that I can output core restaurant details
    ///Acceptance criteria
    ///Must provide valid headers
    ///For known outcode se19, some results are returned
    /// </summary>
    [TestFixture]
    public class Story2Tests
    {
        [TestCase("se19")]
        public void RunMainWithCorrectCodeCheckOutputIntegration(string code)
        {
            var currentConsoleOut = Console.Out;
            using (var consoleOutput = new ConsoleOutput())
            {
                Program.Main(new[] { code });
                
                Assert.False(string.IsNullOrEmpty(consoleOutput.GetOuput()));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestCase("se19", Result = true)]
        [TestCase("somecode", Result = false)]
        public bool JustServiceCallIntegration(string code)
        {
            //Arrange
            var target = new JustEatService(new RestSharp.RestClient("http://api-interview.just-eat.com/"), new RestSharp.RestRequest("restaurants", RestSharp.Method.GET));
            
            //Act
            var restaurants = target.GetRestaurants(code);

            //Assert
            return restaurants.Count() > 0;
        }

        [TestCase("se19")]
        [TestCase("somecode")]
        public void RunMainWithCorrectCodeCheckHeaders(string code)
        {
            //Arange
            var target = new JustEatService(new RestSharp.RestClient("http://api-interview.just-eat.com/"), new RestSharp.RestRequest("restaurants", RestSharp.Method.GET));
            var expectedRequest = new RestSharp.RestRequest("restaurants", RestSharp.Method.GET);
            expectedRequest.AddParameter("q", code);
            expectedRequest.AddHeader("Accept-Tenant", "uk");
            expectedRequest.AddHeader("Accept-Language", "en-GB");
            expectedRequest.AddHeader("Accept-Charset", "utf-8");
            expectedRequest.AddHeader("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");
            expectedRequest.AddHeader("Host", "api-interview.just-eat.com");

            //Act
            var request = target.CreateRequest(code);
            
            //Assert
            Assert.AreEqual(expectedRequest, request);
        }        
    }
}
