<Query Kind="Program" />

void Main()
{
	//Func<int> five = () => 5;
	//five().Dump();
	//
	//Expression<Func<int>> five_exp = () => 5;
	//five_exp.Compile().Invoke().Dump();
	
	var user = new User();
	Expression<Func<User, object>> exp = user => user.Name;
	//var body = exp.Body.Dump();
	//if(body is MemberExpression me){
	//	me.Member.Name.ToLower().Dump();
	//}
	//else if(body is UnaryExpression ue){
	//	((MemberExpression)ue.Operand).Member.Name.ToLower().Dump();
	//}
	exp.Dump();
}

public class User
{
	public string Name { get; set; }
	public int Age { get; set; }
}