using System;
using System.Linq;
using JustEat.RecruitmentTest.App.Business;
using NSubstitute;
using NUnit.Framework;
using RestSharp;

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
            var target = new JustEatService(new RestClient("http://api-interview.just-eat.com/"), new RestRequest("restaurants", Method.GET));
            
            //Act
            var restaurants = target.GetRestaurants(code);

            //Assert
            return restaurants.Any();
        }

        [TestCase("se19")]
        [TestCase("somecode")]
        [TestCase("")]
        public void RunMainWithCorrectCodeCheckHeaders(string code)
        {
            //Arange
            var requestStub = Substitute.For<IRestRequest>();

            var target = new JustEatService(new RestClient("http://api-interview.just-eat.com/"), requestStub);

            //Act
            target.CreateRequest(code);
            
            //Assert
            requestStub.Received().AddHeader("Accept-Tenant", "uk");
            requestStub.Received().AddHeader("Accept-Language", "en-GB");
            requestStub.Received().AddHeader("Accept-Charset", "utf-8");
            requestStub.Received().AddHeader("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");
            requestStub.Received().AddHeader("Host", "api-interview.just-eat.com");
        }        
    }
}
