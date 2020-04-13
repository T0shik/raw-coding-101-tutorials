<Query Kind="Program" />

void Main()
{
//	var service = new HelloService();
//	var consumer = new ServiceConsumer(service);
	var a = typeof(HelloService).Dump();
	var service = (HelloService) Activator.CreateInstance(typeof(HelloService));
	var consumer = (ServiceConsumer) Activator.CreateInstance(typeof(ServiceConsumer), service);
	service.Print();
	consumer.Print();
}

public class ServiceConsumer
{
	HelloService _hello;
	public ServiceConsumer(HelloService hello)
	{
		_hello= hello;
	}
	
	public void Print()
	{
		_hello.Print();
	}
}

public class HelloService
{
	public void Print(){
		"Hello World".Dump();
	}
}