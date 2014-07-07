Did you have time to complete the coding test? What would you add to your solution if you had more time?
better tests, more unit tests (now most tests are integrated), maybe settings loading from xml for request creation

What was the most useful feature in your opinion that was added to C# 4? Please include a snippet of code that shows how you've used it.
dynamic 
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

What's your favourite programming language? Why?
C#. Worked most with it.

How would you track down a performance bottleneck in a .NET application? Have you ever had to do this?
load testing using VS load tests (running load tests for different requests, analyzing why some run slower). Yes, for my diploma project.

How would you improve the Just Eat public API found here?: http://www.just-eat.co.uk/webservice/webservices.asmx
Postcode - map areas (coordinates) conversion. Like for given coordinates, find restaurants.

Please describe yourself using either XML or JSON.
{"Name":"Jane", "Surname":"Radetska","Birth":"30 May 1993","Gender":"Female",Education":{"title":"bachelor of Computer Science","university":"KNU Shevchenko","department":"radiophysics"}, "Job": {"title":".Net developer", "expirience":"2,5 years"}, "hobby":"boogie-woogie dances"}