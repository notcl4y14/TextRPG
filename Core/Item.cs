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
	public string Description = "";
	public int Amount;
	public bool DestroyOnUse = false;

	public string AmountString
	{
		get => Amount != 1 ? $"x{Amount}" : "";
	}

	public virtual void Use(Entity user)
	{
		if (DestroyOnUse)
		{
			Amount--;
		}
	}

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
		catch
		{
			return ItemID.Null;
		}

		return itemID;
	}
}