using JustEat.RecruitmentTest.App.Business;
using JustEat.RecruitmentTest.App.Models;
using NUnit.Framework;

namespace JustEat.RecruitmentTest.App.Tests
{
    /// <summary>
    /// Story 3 - Outputting results
    ///As a user running the command line application
    ///When I search for a valid outcode
    ///I want restaurant details printed into console window
    ///Acceptance criteria
    ///Name, rating and types of food for the restaurant printed into console window
    /// </summary>
    [TestFixture]
    public class Story3Tests
    {
        [Test]
        public void RestarantToStringTest()
        {
            //Arrange
            var restaurant = new Restaurant() { Name = "name", Ratings = 3, CuisineTypes = new[] { "type1", "type2" } };
            const string restToStringExpected = "* Name name \n  Ratings 3 \n  Cuisines type1 type2  \n";
            
            //Act
            var actual = restaurant.ToString();

            //Assert
            Assert.AreEqual(restToStringExpected, actual);
        }
        
        [TestCase("se19")]
        public void CheckThatRestaurantsAreReturned(string code)
        {
            //Arrange
            var target = new JustEatService(new RestSharp.RestClient("http://api-interview.just-eat.com/"), new RestSharp.RestRequest("restaurants", RestSharp.Method.GET));

            //Act
            var restaurants = target.GetRestaurants(code);

            //Assert
            foreach (var restaurant in restaurants)
            {
                Assert.IsNotNullOrEmpty(restaurant.Name);
                Assert.IsNotNull(restaurant.Ratings);
                Assert.IsNotNull(restaurant.CuisineTypes);
            }
        }
    }
}
