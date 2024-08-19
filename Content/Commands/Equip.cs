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
		if (args.Length == 0)
		{
			Console.WriteLine("equip [slotName] [itemID]");
			Console.WriteLine("equip [itemID]");
			Console.WriteLine("Slot Names: attack|weapon");
			return;
		}

		string slotName = "";
		Item? item;
		ItemID itemID;

		if (args.Length == 1)
		{
			itemID = Item.GetIDFromString(args[0]);

			if (itemID == ItemID.Null)
			{
				Console.WriteLine($"There's no {itemID}");
				return;
			}

			item = user.FindItem(itemID);

			if (item == null)
			{
				Console.WriteLine($"You don't have {itemID}");
				return;
			}

			switch (item.Type)
			{
				case ItemType.Weapon:
					slotName = "weapon";
					break;

				default:
					Console.WriteLine($"There are no slots for Item type of {item.Type}");
					return;
			}
		}
		else
		{
			slotName = args[0];
			itemID = Item.GetIDFromString(args[1]);

			if (itemID == ItemID.Null)
			{
				Console.WriteLine($"There's no {itemID}");
				return;
			}

			item = user.FindItem(itemID);

			if (item == null)
			{
				Console.WriteLine($"You don't have {itemID}");
				return;
			}
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