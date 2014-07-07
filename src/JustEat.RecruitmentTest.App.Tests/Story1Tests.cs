using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustEat.RecruitmentTest.App.Business;
using JustEat.RecruitmentTest.App.Models;
using NSubstitute;
using NUnit.Framework;

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
        public void CheckCodeAcceptance(string code)
        {
            //Arrange            
            var stub = Substitute.For<IJustEatService>();
            stub.CreateRequest(code).Returns(new RestSharp.RestRequest());
            stub.GetRestaurants(code).Returns(new Restaurant[0]);
            var app = new Application(stub);

            //Act
            app.Run(code);

            //Assert
            stub.GetRestaurants(code).Received();
            stub.CreateRequest(code).Received();
        }

    }
}
