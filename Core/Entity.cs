namespace Core;

enum EntityID
{
	Null,
	Player
}

class Entity
{
	public EntityID Id;
	public int Health;
	public int HealthMax;
	public List<Item> Inventory = [];
	public int InventoryCapacity;
	public int InventoryCount;

	// Health
	public string HealthString
	{
		get => $"{Health}/{HealthMax}";
	}

	public int HealthPercent
	{
		// https://stackoverflow.com/a/704710/22146374
		get => (int)((float)Health / HealthMax * 100);
	}

	public virtual void OnDead()
	{
		return;
	}

	public void Heal(uint delta)
	{
		Health += (int)delta;

		if (Health > HealthMax)
		{
			Health = HealthMax;
		}
	}

	public void Hurt(uint delta)
	{
		Health -= (int)delta;

		if (Health < 0)
		{
			Health = 0;
			OnDead();
		}
	}

	// Inventory
	public bool IsInventoryFull()
	{
		return Inventory.Count >= InventoryCapacity;
	}
	
	public void AddItem(Item item, int count = 1)
	{
		if (Inventory.Count + 1 > InventoryCapacity)
		{
			return;
		}

		Item? destItem = Inventory.Find(x => x.Id == item.Id);
		if (destItem != null)
		{
			destItem.Amount += count;
			return;
		}

		item.Amount = count;
		Inventory.Add(item);
	}

	public void RemoveItem(ItemID itemID, int count = 1)
	{
		Item? item = Inventory.Find(x => x.Id == itemID);
		
		if (item == null)
		{
			return;
		}
		
		item.Amount -= count;

		if (item.Amount < 1)
		{
			Inventory.Remove(item);
		}
	}

	public void RemoveWholeItem(ItemID itemID)
	{
		Item? item = Inventory.Find(x => x.Id == itemID);

		if (item == null)
		{
			return;
		}
		
		Inventory.Remove(item);
	}

	public void Use(ItemID itemID)
	{
		Item? item = Inventory.Find(x => x.Id == itemID);

		if (item == null)
		{
			return;
		}

		item.Use(this, this);
	}
}