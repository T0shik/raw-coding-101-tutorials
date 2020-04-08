<Query Kind="Program" />

void Main()
{
	Action<string> pipe = (msg) => 
		Try2(msg, (msg1) => 
			Try(msg1, (msg2) => 
				Wrap(msg2, Seccond)));
				
	pipe("1");
}

public void First(string msg)
{
	$"executing {msg}".Dump("first function");
}

public void Seccond(string msg)
{
	$"executing {msg}".Dump("seccond function");
}

public void Wrap(string msg, Action<string> function)
{
	msg.Dump("starting");
	function(msg);
	"ends".Dump();
}

public void Try(string msg, Action<string> function)
{
	try
	{
		msg.Dump("trying");
		function(msg);
	}
	catch (Exception)
	{

	}
}

public void Try2(string msg, Action<string> function)
{
	try
	{
		msg.Dump("trying");
		function(msg);
	}
	catch (Exception)
	{

	}
}
