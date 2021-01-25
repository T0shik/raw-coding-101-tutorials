<Query Kind="Program" />

void Main()
{
	int i = 5;
	GetNumber getN = () => 5 + i;
	getN().Dump();
}

public class MyLambda 
{
	public MyDelegate myDelegate;
	
	public int Main() => 5;
}


public class MyDelegate 
{
	object _obj;
	MethodInfo _info;
	
	public MyDelegate(
		object obj, 
		MethodInfo info
		){
		_obj = obj;
		_info = info;
	}
	
	public int Invoke() => (int) _info.Invoke(_obj, null);
}

delegate int GetNumber();