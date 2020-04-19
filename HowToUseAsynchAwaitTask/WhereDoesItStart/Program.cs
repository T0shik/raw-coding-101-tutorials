using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WhereDoesItStart
{
    class Program
    {
        static void Main(string[] args)
        {
            //StartApp();
            StartApp().GetAwaiter().GetResult();
        }

        public static Task StartApp()
        {
            var collect = Collect();
            var process = Process();
            //Task.WaitAll(new[] { collect, process });

            return Task.WhenAll(new[] { collect, process });
        }

        public static async Task Collect()
        {
            while (true)
            {
                // doing some internet stuff
            }
        }

        public static async Task Process()
        {
            while (true)
            {
                // doing some database stuff

                if (true)
                {
                    //fire and forget
                    Task.Run(() => Notify("hi"));
                }
            }
        }

        public static async Task Notify(string data)
        {
            // some network stuff
        }
    }
}
