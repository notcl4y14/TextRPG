using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class Inventory : Command
{
	public Inventory()
	{
		Name = "inventory";
		Alias = ["invent", "inv"];
	}

	public override void Run(string[] arguments, ref Entity entity)
	{
		TrpgConsole.Inventory(entity.Inventory, entity.InventoryCapacity);
	}
}