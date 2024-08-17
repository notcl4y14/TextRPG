using Core;

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
		Console.WriteLine($"Inventory [{entity.Inventory.Count}/{entity.InventoryCapacity}]:");

		foreach (var item in entity.Inventory)
		{
			Console.WriteLine($"\t- {item.Id}{(item.Amount > 1 ? " " + item.AmountString : "")} {item.Description}");
		}
	}
}