using Content.Items;

namespace Core;

enum ItemID
{
	Null,
	
	Apple,
	Milk,
	Pie,
	PieSlice,
	Water,
	WaterBottle,

	HealthPotion,
	RegenPotion,
	PowerPotion,
	DefensePotion,

	Sword,
	BoneSword,
	Wand,
	FireWand,
	Boulder,
	Rock,

	BronzeArmor,
	IronArmor,
	
	EmptyBottle,
	Grass,
	Fire
}

enum ItemType
{
	Null,
	Food,
	Weapon,
	Armor,
	Misc
}

abstract class Item
{
	public ItemID Id;
	public virtual ItemType Type { get; } = ItemType.Misc;
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
	public virtual void Use(Entity user, Entity target)
	{
		Console.WriteLine($"{Id} doesn't look like so usable");
	}
	
	public virtual void Eat(Entity user, Entity target)
	{
		Console.WriteLine($"{Id} doesn't look so edible");
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