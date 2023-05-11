using System;
using RongChengApp.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore;
using Xunit.Sdk;
using Moq;

namespace RongChengAppTest
{

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var clientFacotry = new Mock<IHttpClientFactory>();
            var client = clientFacotry.Setup(repo => repo.CreateClient()).Callback(a=>a);
            new UtilService(client.);

            Console.WriteLine("ok");

        }
    }
}