<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

HttpClient _client = new HttpClient
{
	Timeout = TimeSpan.FromSeconds(5)
};
SemaphoreSlim _gate = new SemaphoreSlim(20);

void Main()
{
	Task.WaitAll(CreateCalls().ToArray());
}

public IEnumerable<Task> CreateCalls()
{
	for (int i = 0; i < 500; i++)
	{
		yield return CallGoogle();
	}
}


public async Task CallGoogle()
{
	try
	{
		await _gate.WaitAsync();
		var response = await _client.GetAsync("https://google.com");
		_gate.Release();

		response.StatusCode.Dump();
	}
	catch (Exception e)
	{
		e.Message.Dump();
	}
}
