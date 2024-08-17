using Common;
using Core;

namespace Content.Commands;

class Info : Command
{
	public Info()
	{
		Name = "info";
		Alias = [];
	}

	public override void Run(string[] arguments, ref Entity entity)
	{
		Item? item;
		ItemID itemID;
		string itemName;

		if (arguments.Length == 0)
		{
			string input = Game.AskInput("ItemID: ");
			itemName = input;

			itemID = Item.GetIDFromString(input);
			item = ItemLibrary.GetFromID(itemID);
		}
		else
		{
			string input_itemID = arguments[0];
			itemName = input_itemID;

			itemID = Item.GetIDFromString(input_itemID);
			item = ItemLibrary.GetFromID(itemID);
		}

		if (item == null)
		{
			Console.WriteLine($"There's no ItemID {itemName}");
			return;
		}

		Console.WriteLine(item.Name);
		Console.WriteLine("\tDescription: " + item.Description);
		Console.WriteLine("\tStats:");
		Console.WriteLine(item.GetStats("\t\t"));
	}
}