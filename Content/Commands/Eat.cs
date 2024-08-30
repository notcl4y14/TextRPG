using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class Eat : Command
{
	public Eat()
	{
		Name = "eat";
		Alias = [];
	}

	public override void Run(string[] arguments, ref Entity entity)
	{
		Item? item;
		ItemID itemID;
		string itemName;

		if (arguments.Length == 0)
		{
			// Show Inventory
			Console.WriteLine($"You have:");

			foreach (var _item in entity.Inventory.Items)
			{
				Console.WriteLine($"\t- {_item.Id}{(_item.Amount > 1 ? " " + _item.AmountString : "")}");
			}

			string input = TrpgConsole.AskInput("ItemID: ");
			itemName = input;

			itemID = Item.GetIDFromString(input);
			item = entity.FindItem(itemID);
		}
		else
		{
			string input_itemID = arguments[0];
			itemName = input_itemID;
			itemID = Item.GetIDFromString(input_itemID);
			item = entity.FindItem(itemID);
		}

		if (itemID == ItemID.Null)
		{
			Console.WriteLine($"There's no {itemName}");
			return;
		}

		if (item == null)
		{
			Console.WriteLine($"You don't have {itemName}");
			return;
		}

		item.Eat(entity, entity);
	}
}