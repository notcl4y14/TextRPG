using Content.Items;

namespace Core;

enum ItemID
{
	Null,
	Apple
}

abstract class Item
{
	public ItemID Id;
	public string Description;
	public int Amount;

	public string AmountString
	{
		get => Amount != 1 ? $"x{Amount}" : "";
	}

	public abstract void Use(Entity user);

	public static Item? CreateFromID(ItemID ID)
	{
		switch (ID)
		{
			case ItemID.Apple:
				return new Apple();
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
		catch (Exception e)
		{
			return ItemID.Null;
		}

		return itemID;
	}
}