<Query Kind="Program" />

void Main()
{
	var pipe = new PipeBuilder(First)
				.AddPipe(typeof(Try))
				.AddPipe(typeof(Wrap))
				.AddPipe(typeof(Wrap))
				.Build();

	pipe("Wrold");

	pipe("Another One");
}

public void First(string msg)
{
	$"executing {msg}".Dump("first function");
}

public void Seccond(string msg)
{
	$"executing {msg}".Dump("seccond function");
}

public class PipeBuilder
{
	Action<string> _mainAction;
	List<Type> _pipeTypes;
	public PipeBuilder(Action<string> mainAction)
	{
		_mainAction = mainAction;
		_pipeTypes = new List<Type>();
	}

	public PipeBuilder AddPipe(Type pipeType)
	{
		_pipeTypes.Add(pipeType);
		return this;
	}
	
	private Action<string> CreatePipe(int index){
		if(index < _pipeTypes.Count - 1){
			var childPipeHandle = CreatePipe(index + 1);
			var pipe = (Pipe) Activator.CreateInstance(_pipeTypes[index], childPipeHandle);
			return pipe.Handle;
		} else {
			var finalPipe = (Pipe) Activator.CreateInstance(_pipeTypes[index], _mainAction);
			return finalPipe.Handle;
		}
	}
	
	public Action<string> Build(){
		return CreatePipe(0);
	}
}

public abstract class Pipe
{
	protected Action<string> _action;
	public Pipe(Action<string> action)
	{
		_action = action;
	}

	public abstract void Handle(string msg);
}

public class Wrap : Pipe
{
	public Wrap(Action<string> action) : base(action) { }

	public override void Handle(string msg)
	{
		msg.Dump("starting");
		_action(msg);
		"ends".Dump();
	}
}

public class Try : Pipe
{
	public Try(Action<string> action) : base(action) { }

	public override void Handle(string msg)
	{
		try
		{
			msg.Dump("trying");
			_action(msg);
		}
		catch (Exception)
		{

		}
	}
}