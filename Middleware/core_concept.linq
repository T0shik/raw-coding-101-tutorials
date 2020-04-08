<Query Kind="Program" />

void Main()
{
	Try(LambdaFirst);

	Wrap(LambdaSeccond);
}

public void First()
{
	"executing first function".Dump();
}

public void Seccond()
{
	"executing seccond function".Dump();
}

public void LambdaFirst(){
	Wrap(First);
}
public void LambdaSeccond()
{
	Try(Seccond);
}

public void Wrap(Action function)
{
	"start".Dump();
	function();
	"ends".Dump();
}

public void Try(Action function)
{
	try
	{
		"trying".Dump();
		function();
	}
	catch (Exception)
	{

	}
}
