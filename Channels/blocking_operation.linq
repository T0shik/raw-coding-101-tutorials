<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	for(int i = 0; i < 10; i++) {
		i.Dump();
		Task.Run(() => SendNotification());
	}
}

public void SendNotification(){
	Task.Delay(1000).GetAwaiter().GetResult();
	"complete".Dump();
}