using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
            protected readonly TestServer Server;

            protected readonly HttpClient Client;

            //arrange
            protected ControllerTestsBase()
            {
                Server = new TestServer(new WebHostBuilder()
                            .UseStartup<Startup>());
                Client = Server.CreateClient();

            }

            protected static StringContent GetPayload(object data)
            {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
            }

    }
}
