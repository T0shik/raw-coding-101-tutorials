using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AvoidStateMachine : Controller
    {
        public async Task<ActionResult> Index()
        {
            var a = await InputOutputN();
            return View(a);
        }


        public async Task<string> InputOutputN()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync("some site");
            // do something with the content

            return content;
        }

        public Task<string> InputOutputNA()
        {
            var client = new HttpClient();
            return client.GetStringAsync("some site");
        }

        public Task<string> InputOutput()
        {
            var message = "Hello World";
            return Task.FromResult(message);
        }

        public Task InputOutputC()
        {
            return Task.CompletedTask;
        }
    }
}
