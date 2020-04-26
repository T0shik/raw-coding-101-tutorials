using HowToUseChannels.Serivces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HowToUseChannels.Controllers
{
    public class Home : Controller
    {
        public IActionResult Send()
        {
            Task.Run(() =>
            {
                Task.Delay(100).Wait();

                Task.Delay(200).Wait();
            });

            return Ok();
        }

        public Task<bool> SendB([FromServices] Notifications notifications)
        {
            return notifications.Send();
        }

        public bool SendA([FromServices] Notifications notifications)
        {
            return notifications.SendA();
        }

        public async Task<bool> SendC([FromServices] Channel<string> channel)
        {
            await channel.Writer.WriteAsync("Hello");
            return true;
        }
    }
}
