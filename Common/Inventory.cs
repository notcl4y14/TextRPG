using Core;

namespace Common;

class Inventory
{
	public int Capacity;
	public List<Item> Items;

	public Inventory(int capacity)
	{
		Capacity = capacity;
		Items = [];
	}

	public Inventory(List<Item> items, int capacity)
	{
		Capacity = capacity;
		Items = items;
	}

	// ---- Getters ----
	public bool IsFull
	{
		get => Items.Count >= Capacity;
	}

	// ---- Items ----
	public Item FindItem(ItemID itemID)
	{
		return Items.Find(x => x.Id == itemID);
	}

	public bool HasItem(ItemID itemID)
	{
		return Items.Find(x => x.Id == itemID) != null;
	}

	public bool AddItem(Item item, int count = 1)
	{
		// Inventory is full
		if (Items.Count + 1 > Capacity)
		{
			return false;
		}

		// Add more items to existing one
		Item destItem = FindItem(item.Id);

		if (destItem != null)
		{
			destItem.Amount += count;
			return true;
		}

		// Just add the item
		item.Amount = count;
		Items.Add(item);
		return true;
	}

	public void RemoveItem(ItemID itemID, int count = 1)
	{
		Item item = FindItem(itemID);

		if (item == null)
		{
			return;
		}

		item.Amount -= count;

		if (item.Amount < 1)
		{
			Items.Remove(item);
		}
	}
}