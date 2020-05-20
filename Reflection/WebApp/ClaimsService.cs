using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class ClaimsService
    {
        public IEnumerable<string> Claims()
        {
			var authAttr = typeof(AuthorizeAttribute);
			var anonAttr = typeof(AllowAnonymousAttribute);

			return typeof(Startup).Assembly.GetTypes()
				.Where(x => x.Name.EndsWith("Controller"))
				.SelectMany(x => x.GetMethods()
					.Where(m => m.DeclaringType.Equals(x)))
				.Where(x => x.GetCustomAttribute(authAttr) != null
					|| x.DeclaringType.GetCustomAttribute(authAttr) != null)
				.Where(x => x.GetCustomAttribute(anonAttr) == null)
				.Select(x => string.Concat(x.DeclaringType.ToString(),
					".", x.ToString().Split(" ").Last()));
		}
    }
}
