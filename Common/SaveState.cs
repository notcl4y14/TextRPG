using Content.Entities;
using Core;
using IniParser;
using IniParser.Model;

namespace Common;

class SaveState
{
	public static string PlayerName;
	public static string FileName;

	public static bool IsOnSave => PlayerName != null || FileName != null;

	private static Item? ParseItem(string id)
	{
		var itemID = Item.GetIDFromString(id);

		if (itemID == ItemID.Null)
		{
			Console.WriteLine($"There's no ItemID {id}");
			return null;
		}

		return ItemLibrary.GetFromID(itemID);
	}

	private static List<Item> ParseInventory(KeyDataCollection inventory)
	{
		List<Item> result = [];

		foreach (var entry in inventory)
		{
			var item = ParseItem(entry.KeyName);

			if (item == null)
			{
				continue;
			}

			item.Amount = Convert.ToInt32(entry.Value);
			result.Add(item);
		}

		return result;
	}

	public static Player Load(string file)
	{
		IniData data = new FileIniDataParser().ReadFile(file);

		PlayerName = data["main"]["Name"];
		FileName = file;

		int health           = Convert.ToInt32(data["main"]["Health"]);
		int healthMax        = Convert.ToInt32(data["main"]["HealthMax"]);
		Item? attackSlot     = ParseItem(data["main"]["AttackSlot"]);
		Item? armorSlot      = ParseItem(data["main"]["ArmorSlot"]);
		int invCapacity      = Convert.ToInt32(data["main"]["InventoryCapacity"]);
		List<Item> inventory = ParseInventory(data["inventory"]);

        Player controller = new Player
        {
            Health = health,
            HealthMax = healthMax,
            Inventory = new Inventory(inventory, invCapacity),
			AttackSlot = attackSlot
        };

        return controller;
	}

	public static void Save(string name, string file, Entity entity)
	{
		FileIniDataParser parser = new FileIniDataParser();

		PlayerName = name;
		FileName = file;

		var data = new IniData();
		data["main"]["Name"]              = name;
		data["main"]["Health"]            = entity.Health.ToString();
		data["main"]["HealthMax"]         = entity.HealthMax.ToString();
		data["main"]["AttackSlot"]        = entity.AttackSlot?.Id.ToString();
		data["main"]["ArmorSlot"]         = entity.ArmorSlot?.Id.ToString();
		data["main"]["InventoryCapacity"] = entity.Inventory.Capacity.ToString();

		foreach (var item in entity.Inventory.Items)
		{
			data["inventory"][item.Id.ToString()] = item.Amount.ToString();
		}

		parser.WriteFile(file, data);
	}
}