using System.Collections.Generic;
using System.Linq;
using JustEat.RecruitmentTest.App.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;

namespace JustEat.RecruitmentTest.App.Business
{
    public class JustEatService : IJustEatService
    {
        private const string codeParameterName = "q";

        private readonly IRestClient basicClient;
        private readonly IRestRequest emptyRequest;

        private readonly Dictionary<string, string> headers = new Dictionary<string, string>
        {
            {"Accept-Tenant", "uk"},
            {"Accept-Language", "en-GB"},
            {"Accept-Charset", "utf-8"},
            {"Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI="},
            {"Host", "api-interview.just-eat.com"}
        };

        /// <summary>
        ///     Creates JustEatService object, that queries server using
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        public JustEatService(IRestClient client, IRestRequest request)
        {
            basicClient = client;
            emptyRequest = request;
        }

        public virtual IRestRequest CreateRequest(string code)
        {
            IRestRequest request = emptyRequest;
            request.AddParameter(codeParameterName, code);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            return request;
        }

        /// <summary>
        ///     Get restaurants by area's code. Throws JustEatServiceException if it cannot parse server response
        /// </summary>
        /// <param name="code">Code for area where to search restaurants</param>
        /// <returns>Restaurants in code's area</returns>
        public virtual IEnumerable<Restaurant> GetRestaurants(string code)
        {
            IRestResponse response = basicClient.Execute(CreateRequest(code));

            if (string.IsNullOrEmpty(response.Content))
                throw new JustEatServiceException(response.StatusCode, string.Empty);

            //getting response
            var restaurantsResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

            //parsing response
            if (restaurantsResponse == null) throw new JustEatServiceException(response.StatusCode, response.Content);
            
            //we need response.Restaurants be a collection of objects
            var restaurantsData = restaurantsResponse.Restaurants as IEnumerable<dynamic>;
            if (restaurantsData == null) throw new JustEatServiceException(response.StatusCode, response.Content);
            dynamic[] enumeratedRestaurantsData = restaurantsData as dynamic[] ?? restaurantsData.ToArray();

            //we get from server must more data than we need, select only needed - name, ratings, cuisines
            IEnumerable<Restaurant> restaurants = enumeratedRestaurantsData.Select(
                r =>
                {
                    var restaurant = new Restaurant
                    {
                        Name = r.Name,
                        Ratings = r.RatingStars
                    };
                    var cuisines = r.CuisineTypes as IEnumerable<dynamic>;
                    if (cuisines!=null)
                    {
                        restaurant.CuisineTypes = cuisines.Select(ct => (string) ct.Name);
                    }
                    return restaurant;
                });
            return restaurants.OrderByDescending(r => r.Ratings);
        }
    }
}