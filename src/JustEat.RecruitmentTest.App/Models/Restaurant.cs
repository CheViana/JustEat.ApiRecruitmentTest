using System.Collections.Generic;
using System.Linq;

namespace JustEat.RecruitmentTest.App.Models
{
    public class Restaurant
    {
        private const string toStringFormat = "* Name {0} \n  Ratings {1} \n  Cuisines {2} \n";
        public string Name { get; set; }
        public double? Ratings { get; set; }
        public IEnumerable<string> CuisineTypes { get; set; }

        public override string ToString()
        {
            string cuisinesStr = string.Empty;
            if (CuisineTypes != null && CuisineTypes.Any())
                cuisinesStr = CuisineTypes.Aggregate((elem1, elem2) => elem1 + " " + elem2 + " ");
            return string.Format(toStringFormat, Name, Ratings, cuisinesStr);
        }
    }
}