using Core;

namespace Common.Items;

class Weapon : Item
{
	public int Damage;
	public int DamageRange = 5;
	public int CritDamage;
	public int CritChance;

	public override void LoadStats()
	{
		Stats.Add("Damage", "" + Damage);
		Stats.Add("Crit Damage", "" + CritDamage);
		Stats.Add("Crit Chance", "1/" + CritChance);
	}

	private int GetRandomDamage()
	{
		Random random = new Random();
		int delta = random.Next(Damage - DamageRange, Damage + DamageRange);
		return delta;
	}

	private bool ShouldCrit()
	{
		Random random = new Random();
		bool crit = random.Next(CritChance) == 1;
		return crit;
	}

	private int GetRandomCrit()
	{
		Random random = new Random();
		int delta = random.Next(CritDamage - DamageRange, CritDamage + DamageRange);
		return delta;
	}

	public override void Use(Entity user, Entity target)
	{
		int delta = GetRandomDamage();
		bool crit = ShouldCrit();
		if (crit)
		{
			delta = GetRandomCrit();
		}

		Console.WriteLine($"{user} dealt {delta} damage with Sword!");
		Console.Write(crit ? "That was a critical hit!\n" : "");

		target.Hurt((uint)delta);
	}
}