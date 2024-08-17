using Common;
using Content.Entities;
using IniParser;
using IniParser.Model;

namespace Core;

class SaveState
{
	private static List<Item> ParseInventory(KeyDataCollection inventory)
	{
		List<Item> result = [];

		foreach (var entry in inventory)
		{
			var itemID = Item.GetIDFromString(entry.KeyName);

			if (itemID == ItemID.Null)
			{
				Console.WriteLine($"There's no ItemID {entry.KeyName}");
				continue;
			}

			var item = ItemLibrary.GetFromID(itemID);
			item.Amount = int.Parse(entry.Value);
			result.Add(item);
		}

		return result;
	}

	public static Player Load(string file)
	{
		IniData data = new FileIniDataParser().ReadFile(file);

		int health           = int.Parse(data["main"]["Health"]);
		int healthMax        = int.Parse(data["main"]["HealthMax"]);
		int invCapacity      = int.Parse(data["main"]["InventoryCapacity"]);
		List<Item> inventory = ParseInventory(data["inventory"]);

        Player controller = new Player
        {
            Health = health,
            HealthMax = healthMax,
            InventoryCapacity = invCapacity,
            Inventory = inventory
        };

        return controller;
	}

	public static void Save(string name, string file, Entity entity)
	{
		FileIniDataParser parser = new FileIniDataParser();

		var data = new IniData();
		data["main"]["Name"]              = name;
		data["main"]["Health"]            = entity.Health.ToString();
		data["main"]["HealthMax"]         = entity.HealthMax.ToString();
		data["main"]["InventoryCapacity"] = entity.InventoryCapacity.ToString();

		foreach (var item in entity.Inventory)
		{
			data["inventory"][item.Id.ToString()] = item.Amount.ToString();
		}

		parser.WriteFile(file, data);
	}
}