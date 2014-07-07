using System;
using System.IO;

namespace JustEat.RecruitmentTest.App.Tests
{
    public class ConsoleOutput : IDisposable
    {
        private readonly TextWriter originalOutput;
        private readonly StringWriter stringWriter;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }
    }
}