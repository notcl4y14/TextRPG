using Common;
using Core;

namespace Content.Entities;

class Slime : Enemy
{
	public Slime()
	{
		Id = EntityID.Slime;
		Health = 10;
		HealthMax = 10;
		InventoryCapacity = 5;
		Damage = 2;
		DamageRange = 1;
		Cash = new Currency(silver: 10);
	}
	
	public override void Move(Entity[] buddies, Entity opponent)
	{
		Console.WriteLine(Id + " " + Index + " flings its mass at the opponent!");
		base.Move(buddies, opponent);
	}
}