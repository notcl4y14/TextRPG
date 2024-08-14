using Content.Items;

namespace Core;

enum ItemID
{
	Null,
	Apple,
	Sword
}

abstract class Item
{
	public ItemID Id;
	public string Description = "";
	public int Amount;

	public string AmountString
	{
		get => Amount != 1 ? $"x{Amount}" : "";
	}

	public abstract void Use(Entity user, Entity target);

	public void RemoveOne()
	{
		Amount--;
	}

	public static Item? CreateFromID(ItemID ID)
	{
		switch (ID)
		{
			case ItemID.Apple:
				return new Apple();
			case ItemID.Sword:
				return new Sword();
			default:
				return null;
		}
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