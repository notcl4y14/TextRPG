using Common;
using Content.Buffs;
using Core;

namespace Content.Entities;

class FireElemental : Enemy
{
	public FireElemental()
	{
		Id = EntityID.FireElemental;
		Health = 50;
		HealthMax = 50;
		Inventory = new Common.Inventory(4);
		Damage = 4;
		DamageRange = 2;
		Cash = new Currency(gold: 1);
		// TODO: Add the ability to add multiple same items
		// And the ability to set the amount of items
		Items.Add(ItemID.Fire, 2);
		// Items.Add(ItemID.Fire, 12);
		// Items.Add(ItemID.Fire, 24);
	}

	public override void Move(Entity[] buddies, Entity opponent)
	{
		Console.WriteLine(Id + " " + Index + " shoots the fireball at the opponent!");
		base.Move(buddies, opponent);

		Random random = new Random();
		int value = random.Next(4);

		if (value == 1)
		{
			TrpgConsole.MarkupLine(Id + " " + Index + " got its fireball to apply the [rgb(255,157,0)]burning[/] buff!");
			Burn buff = new Burn(4);
			buff.Moves = 2;
			opponent.AddBuff(buff);
		}
	}
}