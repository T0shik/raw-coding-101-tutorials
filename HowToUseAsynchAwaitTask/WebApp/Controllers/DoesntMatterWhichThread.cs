using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DoesntMatterWhichThread : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> InputOutput()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync("some site")
                .ConfigureAwait(false);

            return content;
        }
    }
}
