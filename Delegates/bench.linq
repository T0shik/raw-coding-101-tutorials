<Query Kind="Program">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>BenchmarkDotNet.Jobs</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	BenchmarkRunner.Run<Object_Delegate_Func>().Dump(false);
}

public class Object_Delegate_Func
{
	public class OAdd
	{
		public static int Add(int a, int b) => a + b;
	}
	
	public delegate int Add(int a, int b);
	
	[Benchmark]
	public int Obj(){
		return OAdd.Add(1, 1);
	}

	[Benchmark]
	public int DelObj()
	{
		Add add = OAdd.Add;
		return add(1, 1);
	}
	
	[Benchmark]
	public int DelObjNew()
	{
		var add = new Add(OAdd.Add);
		return add(1, 1);
	}

	[Benchmark]
	public int Del()
	{
		Add add = (a,b) => a + b;
		return add(1,1);
	}

	[Benchmark]
	public int Func()
	{
		Func<int, int, int> add = (a,b) => a + b;
		return add(1,1);
	}
}
