using Core;

namespace Content.Entities;

class Enemy : Entity
{
	public int Index;
	public int Damage;
	public int DamageRange;
	// public int Bronze;
	// public int Silver;
	// public int Gold;

	private int GetRandomDamage()
	{
		Random random = new Random();
		int delta = random.Next(Damage - DamageRange, Damage + DamageRange);
		return delta;
	}

	// public Enemy AssignMoney(int bronze = 0, int silver = 0, int gold = 0)
	// {
	// 	Bronze = bronze;
	// 	Silver = silver;
	// 	Gold = gold;
	// 	return this;
	// }

	public virtual void Move(Entity[] buddies, Entity opponent)
	{
		int damage = GetRandomDamage();

		// Console.WriteLine(Id + " " + Index + " fights back!");
		Console.WriteLine(Id + " " + Index + " deals " + damage + " damage!");
		opponent.Hurt((uint)damage);
	}
}