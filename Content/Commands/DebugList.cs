using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class DebugListItems : Command
{
	public DebugListItems()
	{
		Name = "debug_list_items";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		foreach (var item in ItemLibrary.Registers)
		{
			Console.WriteLine($"{item.Key}: {item.Value}");
		}
	}
}

class DebugListEntities : Command
{
	public DebugListEntities()
	{
		Name = "debug_list_entities";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		foreach (var ent in EntityLibrary.Registers)
		{
			Console.WriteLine($"{ent.Key}: {ent.Value}");
		}
	}
}

class DebugListCommands : Command
{
	public DebugListCommands()
	{
		Name = "debug_list_commands";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		foreach (var command in CommandLibrary.Registers)
		{
			Console.WriteLine($"{command.Key}: {command.Value}");
		}
	}
}