<Query Kind="Program" />

void Main()
{
	// value coming from query/header/body
	//var selectProperty = "word";
	var selectProperty = "number";

	var someClass = new SomeClass { 
		Word = "Hello World", 
		Number = 1234 
	};

	// hard coded, not scalable
	if (selectProperty == "word")
	{
		someClass.Word.Dump();
	}
	else if (selectProperty == "number")
	{
	 	someClass.Number.Dump();
	}
	
	// solution: ???
	
	//parameter
	var parameter = Expression.Parameter(typeof(SomeClass));
	var accessor = Expression.PropertyOrField(parameter, selectProperty);
	
	var lambda = Expression.Lambda(accessor, false, parameter);
	lambda.Compile().DynamicInvoke(someClass).Dump();
}


public class SomeClass
{
	public string Word { get; set; }
	public int Number { get; set; }
}