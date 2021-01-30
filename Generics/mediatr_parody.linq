<Query Kind="Program" />

void Main()
{
	var handler = new RequestHandler();
	handler.Handle(new GetAge()).Dump();
	handler.Handle(new GetName()).Dump();
}

public interface IRequest<T> { }

public class GetAge : IRequest<int> { }

public class GetName : IRequest<string> { }

public interface IHandler { };

public abstract class Handler<T> : IHandler { 
	public abstract T Handle(IRequest<T> request);
};

public abstract class Handler<TRequest, TResponse> : Handler<TResponse>
	where TRequest : IRequest<TResponse>
{
	public override TResponse Handle(IRequest<TResponse> request) => 
		Handle((TRequest) request);
	
	protected abstract TResponse Handle(TRequest requst);
}

public class GetAgeHandler : Handler<GetAge, int>
{
	protected override int Handle(GetAge request) => 20;
}

public class GetNameHandler : Handler<GetName, string>
{
	protected override string Handle(GetName request) => "Foo";
}


public class RequestHandler
{
	public Dictionary<Type, IHandler> requestHandlers = new() {
		[typeof(GetAge)] = new GetAgeHandler(),
		[typeof(GetName)] = new GetNameHandler(),
	};

	public T Handle<T>(IRequest<T> request)
	{
		var handler = requestHandlers[request.GetType()];
		if (handler is Handler<T> h) {
			return h.Handle(request);
		}
		return default;
	}
}


