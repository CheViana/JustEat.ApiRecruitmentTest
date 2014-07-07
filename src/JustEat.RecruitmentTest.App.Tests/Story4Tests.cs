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
        [TestCase("se19")]
        public void CheckRestaurantsOutputtedOnScreen(string code)
        {
            //Arrange
            var target = new JustEatService(new RestSharp.RestClient("http://api-interview.just-eat.com/"), new RestSharp.RestRequest("restaurants", RestSharp.Method.GET));
            var restaurants = target.GetRestaurants(code);
            var restaurantsExpected = new List<string>();
            foreach (var restaurant in restaurants)
            {
                restaurantsExpected.Add(restaurant.ToString().Replace("*",""));
            }


            var currentConsoleOut = Console.Out;
            using (var consoleOutput = new ConsoleOutput())
            {
                //Act
                Program.Main(new[] { code });
                var actual = consoleOutput.GetOuput();
                var parts = actual.Split('*');
                var restaurantsPrinted = parts.Where(p => p.Contains("Name"));
                Assert.AreEqual(restaurantsExpected, restaurantsPrinted);
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestCase("se19")]
        public void CheckRestaurantsOrder(string code)
        {
            //Arrange
            var target = new JustEatService(new RestSharp.RestClient("http://api-interview.just-eat.com/"), new RestSharp.RestRequest("restaurants", RestSharp.Method.GET));
            
            //Act
            var rests = target.GetRestaurants(code);

            //Assert
            for (int i = 0; i < rests.Count()-1; i++)
            {
                Assert.True(rests.ElementAt(i).Ratings >= rests.ElementAt(i+1).Ratings);
            }
        }
    }
}
