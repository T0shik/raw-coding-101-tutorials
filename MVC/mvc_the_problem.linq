<Query Kind="Program" />

void Main()
{
	var uri = new Uri("http://localhost/home/index");
	
	uri.AbsolutePath.Split("/").Skip(1).Dump();
	if(uri.AbsolutePath.StartsWith("/home/index")){
		// execute some loginc
	}
}