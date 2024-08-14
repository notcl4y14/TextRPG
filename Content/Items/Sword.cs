using Core;

namespace Content.Items;

class Sword : Item
{
	public int Damage;
	public int CritDamage;
	public int CritChance;

	public Sword()
	{
		Id = ItemID.Sword;
		Amount = 1;
		Description = "Deals 10 Damage";
		Damage = 10;
		CritDamage = 20;
		CritChance = 4;
	}

    public override void Use(Entity user, Entity target)
    {
		Random random = new Random();
		int delta = random.Next(Damage, Damage + 5);

		bool crit = random.Next(CritChance) == 1;
		if (crit)
		{
			delta = random.Next(CritDamage, CritDamage + 5);
		}

		Console.WriteLine($"{user} dealt {delta} damage with Sword!");
		Console.Write(crit ? "That was a critical hit!\n" : "");

		target.Hurt((uint)delta);
    }
}