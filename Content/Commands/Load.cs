using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class Load : Command
{
	public Load()
	{
		Name = "load";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		if (args.Length < 1)
		{
			Console.WriteLine("load [filename]");
			return;
		}

		entity = SaveState.Load("./" + args[0]);
		Console.WriteLine("Loaded successfully!");
	}
}