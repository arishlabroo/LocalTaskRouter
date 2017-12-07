using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Sockets;
using Xunit;

namespace TaskFeeder
{
    public class FooTask
    {
        public string TaskValue { get; set; }
        public string TaskType { get; set; }
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
                TaskValue = "Babu",
                TaskType = "Regular"
            };

            connection.On<string, string>("BroadcastMessage", (x, y) => { });

            await connection.SendAsync("ReceiveTask", new[] { fooTask });

            Assert.True(true);
        }
    }
}
