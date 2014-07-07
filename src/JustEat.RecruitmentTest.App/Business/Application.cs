using System;
using System.Collections.Generic;
using JustEat.RecruitmentTest.App.Models;

namespace JustEat.RecruitmentTest.App.Business
{
    public class Application
    {
        private readonly IJustEatService justEatService;

        public Application(IJustEatService service)
        {
            justEatService = service;
        }

        public void Run(string codeProvided)
        {
            Console.WriteLine("Enter valid code:");
            string code = codeProvided ?? Console.ReadLine();
            try
            {
                IEnumerable<Restaurant> restaurants = justEatService.GetRestaurants(code);
                Console.WriteLine("Results:");
                foreach (Restaurant restaurant in restaurants)
                {
                    Console.Write(restaurant.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured while quering api, please try again later");
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
            }
            Console.ReadLine();
        }
    }
}