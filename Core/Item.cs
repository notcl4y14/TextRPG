using Content.Items;

namespace Core;

enum ItemID
{
	Null,
	Apple,
	Sword
}

enum ItemType
{
	Null,
	Food,
	Weapon
}

abstract class Item
{
	public ItemID Id;
	public virtual ItemType Type { get; } = ItemType.Null;
	public int Amount = 1;
	public string Name = "";
	public string Description = "";
	public Dictionary<string, string> Stats = [];

	public string AmountString
	{
		get => Amount != 1 ? $"x{Amount}" : "";
	}

	// Library functions
	public Item Load()
	{
		LoadRecipe();
		LoadStats();
		return this;
	}

	public virtual void LoadRecipe() {}
	public virtual void LoadStats() {}

	// Misc.
	public abstract void Use(Entity user, Entity target);

	public void RemoveOne()
	{
		Amount--;
	}

	public string GetStats(string prefix = "")
	{
		string statsString = "";

		foreach (var entry in Stats)
		{
			statsString += $"{prefix}{entry.Key}: {entry.Value}";

			if (entry.Key != Stats.Last().Key)
			{
				statsString += "\n";
			}
		}

		return statsString;
	}

	public static ItemID GetIDFromString(string str)
	{
		ItemID itemID;
		
		try
		{
			itemID = (ItemID)Enum.Parse(typeof(ItemID), str);
		}
		catch
		{
			return ItemID.Null;
		}

		return itemID;
	}
}