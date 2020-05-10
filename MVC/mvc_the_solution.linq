<Query Kind="Program">
  <Namespace>System.Web</Namespace>
</Query>

void Main()
{
	var uri = new Uri("http://localhost/home/index?msg=Hello&num=5");

	var container = new MvcContainer();
	var result = container.Resolve(uri);
	result.Dump();
}

public class MvcContainer {
	
	List<Type> controllerTypes = new List<Type>();
	
	public MvcContainer()
	{
		var controllerType = typeof(Controller);

		controllerTypes = controllerType.Assembly.GetTypes()
			.Where(type => !type.IsAbstract
				&& controllerType.IsAssignableFrom(type))
				.ToList();
	}
	
	public object Resolve(Uri uri){
		var controller = getController(uri);
		var action = getAction(controller, uri);
		var parameters = getParameters(action, uri);
		return action.Invoke(controller, parameters);
	}

	private object[] getParameters(MethodInfo methodInfo, Uri uri)
	{
		var parameterInfos = methodInfo.GetParameters().ToList();
		if(parameterInfos.Count == 0){
			return null;
		}
		var results = new object[parameterInfos.Count];
		
		var query = HttpUtility.ParseQueryString(uri.Query);
		
		for(int i = 0; i < parameterInfos.Count; i++){
			var info = parameterInfos[i];
			var type = parameterInfos[i].ParameterType;
			var value = query[info.Name];
			if(type == typeof(String)){
				results[i] = value;
			}
			else if (type == typeof(Int32))
			{
				results[i] = Int32.Parse(value);
			}
		}
		
		return results;
	}

	private MethodInfo getAction(Controller controller, Uri uri)
	{
		var action = uri.AbsolutePath.Split('/').Last();
		
		return controller.GetType().GetMethods()
			.FirstOrDefault(x => x.Name.Equals(action, 
				StringComparison.InvariantCultureIgnoreCase));
	}

	public Controller getController(Uri uri){
		var controllerType = controllerTypes
			.FirstOrDefault(x => uri.AbsolutePath
				.StartsWith($"/{x.Name.Replace("Controller", "")}", 
					StringComparison.InvariantCultureIgnoreCase));
					
		
		return (Controller) Activator.CreateInstance(controllerType, null);
	}

}

public abstract class Controller{}

public class HomeController : Controller {

	public string Index(int num, string msg)
	{
		return $"Hello World {msg} - {num}";
	}

	public string Test()
	{
		return "Hello Test";
	}
}

public class TestController : Controller
{

	public string Index()
	{
		return "Test World";
	}

}