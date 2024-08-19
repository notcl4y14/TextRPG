using Core;

namespace Content.Commands;

class Equip : Command
{
	public Equip()
	{
		Name = "equip";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity user)
	{
		if (args.Length < 2)
		{
			Console.WriteLine("equip [slotName] [itemID]");
			Console.WriteLine("Slot Names: attack|weapon");
			return;
		}

		string slotName = args[0];

		ItemID itemID = Item.GetIDFromString(args[1]);

		if (itemID == ItemID.Null)
		{
			Console.WriteLine($"There's no {itemID}");
			return;
		}

		Item? item = user.FindItem(itemID);

		if (item == null)
		{
			Console.WriteLine($"You don't have {itemID}");
			return;
		}

		switch (slotName.ToLower())
		{
			case "attack":
			case "weapon":
				user.AttackSlot = item;
				break;
			
			default:
				Console.WriteLine($"No slot named {slotName}");
				break;
		}
	}
}