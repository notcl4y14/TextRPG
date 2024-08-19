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
	}
}