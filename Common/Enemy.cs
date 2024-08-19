using Core;

namespace Content.Entities;

class Enemy : Entity
{
	public int Damage;
	public int DamageRange;

	private int GetRandomDamage()
	{
		Random random = new Random();
		int delta = random.Next(Damage - DamageRange, Damage + DamageRange);
		return delta;
	}

	public void Move(Entity[] buddies, Entity opponent)
	{
		int damage = GetRandomDamage();

		Console.WriteLine(Id + " fights back!");
		Console.WriteLine(Id + " deals " + damage + " damage!");
		opponent.Hurt((uint)damage);
	}
}