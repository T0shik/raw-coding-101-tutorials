<Query Kind="Program" />

void Main()
{
	//var enumrable = Get();
	//var list = Get().ToList();

	//var count = list.Count;
	//count.Dump("count");
	//foreach(var e in list){
	//	e.Dump("value");
	//}

	Get()
	.Where(num => num.Dump("where") < 10)
	.Select(num => num.Dump("select"))
	.Count();

}

public IEnumerable<int> Get()
{
	"1".Dump();
	yield return 1;
	"2".Dump();
	yield return 2;
	"3".Dump();
}

