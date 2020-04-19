using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DontAsyncInConstructor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Task<string> InputOutput()
        {

        }
    }

    public class SomeObject
    {
        public SomeObject()
        {
            //never do async here.
        }

        public static async Task<SomeObject> Create()
        {
            return new SomeObject();
        }
    }

    public class SomeObjectFactory
    {
        public SomeObjectFactory()
        {

        }

        public async Task<SomeObject> Create()
        {
            return new SomeObject();
        }
    }
}
