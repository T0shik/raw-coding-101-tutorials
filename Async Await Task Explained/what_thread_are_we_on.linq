<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	//part 1
	Thread.CurrentThread.ManagedThreadId.Dump("1");
	var client = new HttpClient();
	Thread.CurrentThread.ManagedThreadId.Dump("2");
	var task = client.GetStringAsync("http://google.com");
	Thread.CurrentThread.ManagedThreadId.Dump("3");

	var a = 0;
	for (int i = 0; i < 1_000_000; i++)
	{
		a += i;
	}

	Thread.CurrentThread.ManagedThreadId.Dump("4");
	var page = await task;
	// part 2
	Thread.CurrentThread.ManagedThreadId.Dump("5");
}