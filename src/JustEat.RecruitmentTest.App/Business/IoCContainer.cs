using StructureMap;
using RestSharp;

namespace JustEat.RecruitmentTest.App.Business
{
    public static class IoCContainer
    {
        public static void Init()
        {
            ObjectFactory.Initialize(x => x.For<IJustEatService>().Use<JustEatService>()
                .Ctor<IRestClient>("client").Is(new RestClient("http://api-interview.just-eat.com/"))
                .Ctor<IRestRequest>("request").Is(new RestRequest("restaurants",Method.GET)));
        }
    }
}
