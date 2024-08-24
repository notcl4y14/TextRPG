using Common.Items;
using Core;
using Core.Cli;

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
			Console.WriteLine("Slot Names: attack|weapon, armor");
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
				Console.WriteLine($"There's no {args[0]}");
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
				case ItemType.Armor:
					slotName = "armor";
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
				Console.WriteLine($"There's no {args[1]}");
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
			case "armor":
				if (!(item is Armor))
				{
					Console.WriteLine($"{itemID} is not armor!");
					break;
				}

				user.ArmorSlot = (Armor)item;
				break;

			default:
				Console.WriteLine($"No slot named {slotName}");
				break;
		}
	}
}