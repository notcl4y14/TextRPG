using Core;

namespace Common;

class Enemy : Entity
{
	public int Index;
	public int Damage;
	public int DamageRange;
	public Dictionary<ItemID, int> Items = [];
	
	private int GetRandomDamage()
	{
		Random random = new Random();
		int delta = random.Next(Damage - DamageRange, Damage + DamageRange);
		return delta;
	}

	public virtual void Move(Entity[] buddies, Entity opponent)
	{
		int damage = GetRandomDamage();

		// Console.WriteLine(Id + " " + Index + " fights back!");
		Console.WriteLine(Id + " " + Index + " deals " + damage + " damage!");
		Attack(opponent, damage);
	}
}