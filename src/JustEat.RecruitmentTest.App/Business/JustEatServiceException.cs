using System;
using System.Net;

namespace JustEat.RecruitmentTest.App.Business
{
    public class JustEatServiceException : Exception
    {
        private readonly string message;

        public JustEatServiceException(HttpStatusCode responseStatus, string responseBody)
        {
            message = "Exception. Response from server: " + responseStatus + " " + responseBody;
        }

        public override string Message
        {
            get { return message; }
        }
    }
}