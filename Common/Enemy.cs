using Core;

namespace Content.Entities;

class Enemy : Entity
{
	public int Damage;
	public int DamageRange;
	public int Index;

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
		opponent.Hurt((uint)damage);
	}
}