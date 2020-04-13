<Query Kind="Program" />

void Main()
{
	var container = new DependencyContainer();
	container.AddTransient<ServiceConsumer>();
	container.AddTransient<HelloService>();
	container.AddSingleton<MessageService>();

	var resolver = new DependencyResolver(container);

	var service1 = resolver.GetService<ServiceConsumer>();

	service1.Print();
	var service2 = resolver.GetService<ServiceConsumer>();
	service2.Print();
	var service3 = resolver.GetService<ServiceConsumer>();
	service3.Print();
}

public class DependencyResolver
{
	DependencyContainer _container;
	public DependencyResolver(DependencyContainer container)
	{
		_container = container;
	}

	public T GetService<T>()
	{
		return (T)GetService(typeof(T));
	}

	public object GetService(Type type)
	{
		var dependency = _container.GetDependency(type);
		var constructor = dependency.Type.GetConstructors().Single();
		var parameters = constructor.GetParameters().ToArray();

		if (parameters.Length > 0)
		{
			var parameterImplementations = new object[parameters.Length];

			for (int i = 0; i < parameters.Length; i++)
			{
				parameterImplementations[i] = GetService(parameters[i].ParameterType);
			}

			return CreateImplementaiton(dependency, t => Activator.CreateInstance(t, parameterImplementations));
		}

		return CreateImplementaiton(dependency, t => Activator.CreateInstance(t));
	}

	public object CreateImplementaiton(Dependency dependency, Func<Type, object> factory)
	{
		if (dependency.Implemented)
		{
			return dependency.Implementation;
		}

		var implementation = factory(dependency.Type);

		if (dependency.Lifetime == DependencyLifetime.Singleton)
		{
			dependency.AddImplementation(implementation);
		}

		return implementation;
	}
}


public class DependencyContainer
{
	List<Dependency> _dependencies;

	public DependencyContainer()
	{
		_dependencies = new List<Dependency>();
	}

	public void AddSingleton<T>()
	{
		_dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Singleton));
	}

	public void AddTransient<T>()
	{
		_dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Transient));
	}

	public Dependency GetDependency(Type type)
	{
		return _dependencies.First(x => x.Type.Name == type.Name);
	}
}

public class Dependency
{
	public Dependency(Type t, DependencyLifetime l)
	{
		Type = t;
		Lifetime = l;
	}
	public Type Type { get; set; }
	public DependencyLifetime Lifetime { get; set; }
	public object Implementation { get; set; }
	public bool Implemented { get; set; }
	
	public void AddImplementation(object i){
		Implementation = i;
		Implemented = true;
	}
}

public enum DependencyLifetime
{
	Singleton = 0,
	Transient = 1,
}

public class ServiceConsumer
{
	HelloService _hello;
	public ServiceConsumer(HelloService hello)
	{
		_hello = hello;
	}

	public void Print()
	{
		_hello.Print();
	}
}

public class HelloService
{
	MessageService _message;
	int _random;
	public HelloService(MessageService message)
	{
		_message = message;
		_random = new Random().Next();
	}

	public void Print()
	{
		$"Hello #{_random} World {_message.Message()}".Dump();
	}
}

public class MessageService
{
	int _random;
	public MessageService()
	{
		_random = new Random().Next();
	}

	public string Message()
	{
		return $"Yo #{_random}";
	}
}