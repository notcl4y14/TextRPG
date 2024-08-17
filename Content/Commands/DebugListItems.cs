using Common;
using Core;

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