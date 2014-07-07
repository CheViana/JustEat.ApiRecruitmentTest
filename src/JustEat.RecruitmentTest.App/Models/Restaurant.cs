using System.Collections.Generic;
using System.Linq;

namespace JustEat.RecruitmentTest.App.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public double? Ratings { get; set; }
        public IEnumerable<string> CuisineTypes { get; set; }

        private const string toStringFormat = "* Name {0} \n Ratings {1} \n Cuisines {2} \n";

        public override string ToString()
        {
            return string.Format(toStringFormat, Name, Ratings, CuisineTypes.Aggregate((elem1, elem2) => elem1 + " " + elem2 + " "));
        }
    }
}