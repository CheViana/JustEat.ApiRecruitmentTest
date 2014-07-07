using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustEat.RecruitmentTest.App.Models;
using Newtonsoft.Json;
using RestSharp;
using StructureMap;

namespace JustEat.RecruitmentTest.App.Business
{
    public class JustEatService : IJustEatService
    {
        private readonly IRestRequest emptyRequest;
        private readonly IRestClient basicClient;
        public JustEatService(IRestClient client, IRestRequest request)
        {
            basicClient = client;
            emptyRequest = request;
        }
        
        public IRestRequest CreateRequest(string code)
        {
            var request = emptyRequest;            
            request.AddParameter("q", code);
            request.AddHeader("Accept-Tenant", "uk");
            request.AddHeader("Accept-Language", "en-GB");
            request.AddHeader("Accept-Charset", "utf-8");
            request.AddHeader("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");
            request.AddHeader("Host", "api-interview.just-eat.com");
            return request;

        }
        public IEnumerable<Restaurant> GetRestaurants(string code)
        {
            IRestResponse response = basicClient.Execute(CreateRequest(code));

            if (string.IsNullOrEmpty(response.Content)) return new Restaurant[0]; //empty result
            
            //getting response
            var restaurantsResponse =
                    JsonConvert.DeserializeObject<dynamic>(response.Content);

            //parsing response
            var restaurants = (restaurantsResponse.Restaurants as IEnumerable<dynamic>).Select(
                r => new Restaurant
                {
                    Name = r.Name,
                    Ratings = r.RatingStars, 
                    CuisineTypes = (r.CuisineTypes as IEnumerable<dynamic>).Select(ct=>(string)ct.Name)
                });
            return restaurants.OrderByDescending(r=>r.Ratings);
        }
    }
}
