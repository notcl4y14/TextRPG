using Common;
using Core;

namespace Content.Entities;

class Skeleton : Enemy
{
	public Skeleton()
	{
		Id = EntityID.Skeleton;
		Health = 75;
		HealthMax = 75;
		InventoryCapacity = 75;
		Damage = 15;
		DamageRange = 5;
		Cash = new Currency(silver: 25);
		Items.Add(ItemID.BoneSword, 8);
	}

	public override void Move(Entity[] buddies, Entity opponent)
	{
		Console.WriteLine(Id + " " + Index + " punches the opponent with his bones!");
		base.Move(buddies, opponent);
	}
}