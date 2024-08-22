using Common;
using Core;

namespace Content.Commands;

class Shop : Command
{
	public Shop()
	{
		Name = "shop";
		Alias = ["buy"];
	}

	public override void Run(string[] args, ref Entity user)
	{
		Dictionary<ItemID, Currency> items = Common.Shop.Items;

		foreach (var entry in items)
		{
			ItemID entryItem = entry.Key;
			Currency price = entry.Value;

			Console.WriteLine($"- {entryItem}: {price.Present()}");
		}

		string input_itemID = Log.AskInput("Choose Item: ");
		ItemID itemID = Item.GetIDFromString(input_itemID);

		if (itemID == ItemID.Null)
		{
			Console.WriteLine($"There's no {input_itemID}");
			return;
		}

		if (!Common.Shop.Items.ContainsKey(itemID))
		{
			Console.WriteLine($"You don't see {itemID}");
			return;
		}

		// int amount;
		// string input_amount = Log.AskInput("Amount (Leave blank for 1): ");

		// try
		// {
		// 	if (input_amount == "")
		// 	{
		// 		amount = 1;
		// 	}
		// 	else
		// 	{
		// 		amount = Convert.ToInt32(input_amount);
		// 	}
		// }
		// catch
		// {
		// 	Console.WriteLine("Invalid input");
		// 	return;
		// }

		Item? item = Common.Shop.BuyItem(itemID, ref user.Cash);

		if (item == null)
		{
			Console.WriteLine("Too low on money");
			return;
		}
		
		user.AddItem(item);
		Console.WriteLine($"Bought {itemID} for {Common.Shop.Items[itemID].Present()}!");
	}
}