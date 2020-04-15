using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
			var client = new HttpClient();
			var task = await client.GetStringAsync("https://google.com");

			int a = 0;
			for (int i = 0; i < 1_000_000; i++)
			{
				a = i + 1;
			}
			var task2 = await client.GetStringAsync("https://google.com");

			Console.WriteLine("Hello World");
		}
    }
}
