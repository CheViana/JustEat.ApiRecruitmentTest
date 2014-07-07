using JustEat.RecruitmentTest.App.Business;
using StructureMap;

namespace JustEat.RecruitmentTest.App
{
    public class Program
    {
        static Program()
        {
            IoCContainer.Init();
        }

        public static void Main(string[] args)
        {
            var app = new Application(ObjectFactory.GetInstance<IJustEatService>());
            app.Run(args.Length > 0 ? args[0] : null);
        }
    }
}