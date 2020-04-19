using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DontBlockTheThread : Controller
    {
        public IActionResult Index()
        {
            var task = InputOutput();

            //BAD
            var a = task.Result;

            //BAD
            task.Wait();

            //BAD
            task.GetAwaiter().GetResult();

            //solution is to propagate async await task throught your code

            return View();
        }

        public Task<string> InputOutput()
        {
            var client = new HttpClient();
            return client.GetStringAsync("some site");
        }
    }
}
