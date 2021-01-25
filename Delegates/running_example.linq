<Query Kind="Program" />

public delegate void DrinkingAction();

void Main()
{
	//DrinkWater();
	//DrinkBeer();
	//DrinkLemonade();
	
	RelaxingOnTheBeach(DrinkWater);
	RelaxingOnTheBeach(DrinkBeer);
	RelaxingOnTheBeach(DrinkLemonade);
	//Run(DrinkBeer);
	
	//var drinkingAction = new DrinkingAction(DrinkWater);

	Func<int, int , int> add = (int a, int b) => a + b;
	
	add(1,1).Dump();
	
	Action<int, int> printNumber = (n, n1) => n.Dump();
}

public void DrinkWater() => "Drinking Water".Dump();
public void DrinkBeer() => "Drinking Beer".Dump();
public void DrinkLemonade() => "Drinking Lemonade".Dump();


public void RelaxingOnTheBeach(Action drink){
	"Realxing on the Beach".Dump();
	if(drink != null) drink();
}

public void Run(Func<int> drink)
{
	"Running 10 miles".Dump();
	if (drink != null) drink();
}