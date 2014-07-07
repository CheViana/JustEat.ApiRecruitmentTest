using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Enter valid postcode:");
            var code = codeProvided ?? Console.ReadLine();            
            var restaurants = justEatService.GetRestaurants(code);
            Console.WriteLine("Results:");
            foreach (var restaurant in restaurants)
            {
                Console.Write(restaurant.ToString());
            }
            Console.ReadLine();
        }
    }
}
