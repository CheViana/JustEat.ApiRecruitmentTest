using System.Collections.Generic;
using JustEat.RecruitmentTest.App.Models;
using RestSharp;

namespace JustEat.RecruitmentTest.App.Business
{
    public interface IJustEatService
    {
        IRestRequest CreateRequest(string code);
        IEnumerable<Restaurant> GetRestaurants(string code);
    }
}