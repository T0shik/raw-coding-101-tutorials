using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        [HttpGet()]
        public string Secure()
        {
            return "";
        }

        [HttpGet("{boi}")]
        public string Secure(string boi)
        {
            return "";
        }

        public string Secret()
        {
            return "";
        }

        [AllowAnonymous]
        public string Allowed()
        {
            return "";
        }
    }
}
