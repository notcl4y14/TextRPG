using Common;
using Core;

namespace Content.Commands;

class DebugAdd : Command
{
	public DebugAdd()
	{
		Name = "debug_add";
		Alias = [];
	}

	public override void Run(string[] args, Entity entity)
	{
		ItemID itemID;
		Item? item;
		string itemName;

		if (args.Length == 0)
		{
			string input = Game.AskInput("ItemID: ");
			itemName = input;

			itemID = Item.GetIDFromString(input);
			item = ItemLibrary.GetFromID(itemID);
		}
		else
		{
			string input_itemID = args[0];
			itemName = input_itemID;

			itemID = Item.GetIDFromString(input_itemID);
			item = ItemLibrary.GetFromID(itemID);
		}

		if (item == null)
		{
			Console.WriteLine($"There's no ItemID {itemName}");
			return;
		}

		if (entity.Inventory.Count >= entity.InventoryCapacity)
		{
			Console.WriteLine("Inventory is full!");
			return;
		}

		Console.WriteLine($"Successfully added {itemName}!");
		entity.AddItem(item);
	}
}