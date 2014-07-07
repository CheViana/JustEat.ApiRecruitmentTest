using System;
using System.Globalization;
using System.Linq;
using JustEat.RecruitmentTest.App.Business;
using JustEat.RecruitmentTest.App.Models;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using RestSharp;

namespace JustEat.RecruitmentTest.App.Tests
{
    /// <summary>
    /// Story 4 - Sorting results
    ///As a user running the command line application
    ///When I output core restaurant details
    ///I want to see restaurants ordered correctly
    ///Acceptance criteria    
    ///Restaurants ordered and outputted on screen
    ///Restaurants ordered highest to lowest
    /// </summary>
    [TestFixture]
    public class Story4Tests
    {
        //test for displaying on console
        [TestCase("se19")]
        public void CheckRestaurantsOutputtedOnScreen(string code)
        {

            if(CultureInfo.InstalledUICulture.Name!="ua-UA" && CultureInfo.InstalledUICulture.Name!="ru-RU") Assert.Inconclusive("Another system language then the required for this test (ua or ru)");
            
            //Arrange
            var stubbedRestaurants = new[]
            {
                new {Name = "U Holema", RatingStars = 5.0, CuisineTypes = new dynamic[] {new{Name="Czech"}, new{Name="Beer pub"}}},
                 new {Name = "BeerTime", RatingStars = 3.7, CuisineTypes = new dynamic[] {new{Name="European"}}}
            };
            var stubbedResponseContent = new {Restaurants = stubbedRestaurants};
            var stubResponse = new RestResponse {Content = JsonConvert.SerializeObject(stubbedResponseContent)};
            var stubClient = Substitute.For<IRestClient>();
            stubClient.Execute(new RestRequest()).ReturnsForAnyArgs(stubResponse);
            var service = new JustEatService(stubClient, new RestRequest());
            var currentConsoleOut = Console.Out;
            using (var consoleOutput = new ConsoleOutput())
            {
                var application = new Application(service);
                
                //Act
                application.Run(code);
                var actual = consoleOutput.GetOuput();
                
                //Assert
                foreach (var rest in stubbedRestaurants)
                {
                    Assert.True(actual.Contains(rest.Name));
                    Assert.True(actual.Contains(rest.RatingStars+""));
                    foreach(var cuisine in rest.CuisineTypes)
                    Assert.True(actual.Contains((string)cuisine.Name));
                }
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        //test for ordering
        [TestCase("se19")]
        public void CheckRestaurantsOrder(string code)
        {
            //Arrange
            var target = new JustEatService(new RestClient("http://api-interview.just-eat.com/"), new RestRequest("restaurants", Method.GET));
            
            //Act
            var rests = target.GetRestaurants(code);

            //Assert
            var restaurants = rests as Restaurant[] ?? rests.ToArray();
            for (int i = 0; i < restaurants.Count()-1; i++)
            {
                Assert.True(restaurants.ElementAt(i).Ratings >= restaurants.ElementAt(i+1).Ratings);
            }
        }
    }
}
