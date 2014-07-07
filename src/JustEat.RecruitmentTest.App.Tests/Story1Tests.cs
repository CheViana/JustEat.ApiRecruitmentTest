using JustEat.RecruitmentTest.App.Business;
using JustEat.RecruitmentTest.App.Models;
using NSubstitute;
using NUnit.Framework;
using RestSharp;

namespace JustEat.RecruitmentTest.App.Tests
{
    /// <summary>
    /// Story 1 - Accepting command line input
    /// As a user running the command line application
    /// I can supply a valid outcode on the command line
    /// So that I can query the JUST EAT API for results
    /// Acceptance criteria
    /// Command line parameter accepted
    /// </summary>
    [TestFixture]
    public class Story1Tests
    {
        [TestCase("se19")]
        [TestCase("somecode")]
        [TestCase("")]
        public void CheckCodeAcceptanceInService(string code)
        {
            //Arrange            
            var stub = Substitute.For<IJustEatService>();
            stub.GetRestaurants(code).Returns(new Restaurant[0]);
            var app = new Application(stub);

            //Act
            app.Run(code);

            //Assert
            stub.Received().GetRestaurants(code);
        }


        [TestCase("se19")]
        [TestCase("somecode")]
        [TestCase("")]
        public void CheckCodeAppearInRequest(string code)
        {
            //Arange
            var requestStub = Substitute.For<IRestRequest>();

            var target = new JustEatService(new RestClient("http://api-interview.just-eat.com/"), requestStub);

            //Act
            target.CreateRequest(code);

            //Assert
            requestStub.Received().AddParameter("q", code);
        }
    }
}
