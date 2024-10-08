using Core;
using Core.Cli;

namespace Common;

class ItemLibrary : Library<ItemID, Item>;

class CraftLibrary : Library<ItemID, CraftRecipe>
{
	public static CraftRecipe? GetRecipe(ItemID itemID)
	{
		if (!Registers.ContainsKey(itemID))
		{
			return null;
		}
		
		return Registers[itemID];
	}
}

class EntityLibrary : Library<EntityID, Entity>;

class EnemyLibrary : Library<EntityID, Enemy>
{
	public static Enemy GetRandom()
	{
		Random random = new Random();
		int index = random.Next(Registers.Count);
		Enemy enemy = Registers.ElementAt(index).Value;
		return enemy;
	}
}

class CommandLibrary : Library<string, Command>
{
	public static Dictionary<string, string[]> Alias = [];

	private static string? SearchAlias(string id)
	{
		foreach (var entry in Alias)
		{
			if (entry.Value.Contains(id))
			{
				return entry.Key;
			}
		}

		return null;
	}

	public static new bool Register(string id, Command value)
	{
		if (Registers.ContainsKey(id))
		{
			return false;
		}

		if (value.Alias.Length > 0)
		{
			Alias[id] = value.Alias;
		}

		Registers[id] = value;
		return true;
	}

	public static new Command? GetFromID(string id)
	{
		if (!Registers.ContainsKey(id))
		{
			string? aliasID = SearchAlias(id);
			Command? alias = aliasID != null
				? Registers[aliasID]
				: null;
			
			return alias != null
				? alias
				: default;
		}

		return Registers[id];
	}
}