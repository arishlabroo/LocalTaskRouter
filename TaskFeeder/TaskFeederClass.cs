using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Sockets;
using Xunit;

namespace TaskFeeder
{
    public class FooTask
    {
        public int MyProperty { get; set; }
        public string Name { get; set; }
    }

    public class TaskFeederClass
    {
        [Fact]
        public async Task Test1()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/task")
                .WithConsoleLogger()
                .WithJsonProtocol()
                .WithTransport(TransportType.WebSockets)
                .Build();

            await connection.StartAsync();

            var fooTask = new FooTask
            {
                Name = "Babu",
                MyProperty = 22
            };

            connection.On<string, string>("BroadcastMessage", (x, y) => { });

            await connection.SendAsync("Receive", new[] { fooTask });

            Assert.True(true);
        }
    }
}
