using Common;
using Common.Items;

namespace Core;

enum EntityID
{
	Null,
	Player,
	Slime,
	Skeleton
}

class Entity
{
	public EntityID Id;
	public int Health;
	public int HealthMax;
	public Currency Cash;
	public Inventory Inventory;
	public Item? AttackSlot;
	public Armor? ArmorSlot;

	public List<Buff> Buffs = [];
	public int AddDefense = 0;

	// ---- Slots ----
	public string AttackSlotString => AttackSlot != null
		? AttackSlot.Id.ToString() : "[ItemsNone]None[/]";
	
	public string ArmorSlotString => ArmorSlot != null
		? ArmorSlot.Id.ToString() : "[ItemsNone]None[/]";
	
	public int Defense
	{
		get => (ArmorSlot != null ? ArmorSlot.Defense : 0) + AddDefense;
	}

	// ---- Cash ----
	public string CashString => Cash.Present();

	// ---- Health ----
	public string HealthString
	{
		get => $"{Health}/{HealthMax}";
	}

	public int HealthPercent
	{
		// https://stackoverflow.com/a/704710/22146374
		get => (int)((float)Health / HealthMax * 100);
	}

	public bool IsDead
	{
		get => Health <= 0;
	}

	public virtual void OnDead()
	{
		Console.WriteLine($"{Id} is ded");
	}

	public virtual void OnHurt(Entity sender, int damage)
	{
		Console.WriteLine($"{Id} loses {damage} HP! ({Health}/{HealthMax})");
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
		int damage = (int)delta - Defense;
		damage = damage < 0 ? 1 : damage;
		
		Health -= damage;

		if (Health <= 0)
		{
			Health = 0;
			OnDead();
		}
	}

	// ---- Inventory ----
	public string InvCapacityString => Inventory.Items.Count + "/" + Inventory.Capacity;

	public bool IsInvFull { get => Inventory.IsFull; }

	public Item FindItem(ItemID itemID) => Inventory.FindItem(itemID);

	public bool HasItem(ItemID itemID) => Inventory.HasItem(itemID);

	public bool AddItem(Item item, int count = 1) => Inventory.AddItem(item, count);

	public void RemoveItem(ItemID itemID, int count = 1) => Inventory.RemoveItem(itemID, count);

	public void Use(ItemID itemID, Entity target)
	{
		Item? item = Inventory.FindItem(itemID);

		if (item == null)
		{
			return;
		}

		item.Use(this, target);
	}

	// ---- Buffs ----
	public bool HasBuff(BuffID buffID)
	{
		return Buffs.Find(buff => buff.Id == buffID) != null;
	}

	public void AddBuff(Buff buff)
	{
		if (HasBuff(buff.Id))
		{
			return;
		}

		Buffs.Add(buff);
	}
}