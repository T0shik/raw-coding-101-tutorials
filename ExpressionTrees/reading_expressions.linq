<Query Kind="Program" />

void Main()
{
	string url = "http://example.com/users";

	CreateUrl(url, "name", "age").Dump("1"); // mistake prone
	
	CreateUrl(url, u => u.Name, u => u.Age).Dump("2");
}

public string CreateUrl(string url, params string[] fields){
	var selectedFields = string.Join(',', fields);
	return string.Concat(url, "?fields=", selectedFields);
}

public string CreateUrl(string url, params Expression<Func<User, object>>[] fieldSelectors){
	var fields = new List<string>();
	
	foreach(var selector in fieldSelectors){
		var body = selector.Body;
		if (body is MemberExpression me)
		{
			fields.Add(me.Member.Name.ToLower());
		}
		else if (body is UnaryExpression ue)
		{
			fields.Add(((MemberExpression)ue.Operand).Member.Name.ToLower());
		}
	}
	var selectedFields = string.Join(',', fields);
	return string.Concat(url, "?fields=", selectedFields);
}


public class User {
	public string Name { get; set; }
	public int Age { get; set; }
}