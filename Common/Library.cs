using Core;

namespace Common;

class ItemLibrary : Library<ItemID, Item>;

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