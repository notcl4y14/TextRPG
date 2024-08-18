using Core;

namespace Content.Entities;

class Slime : Entity
{
	public Slime()
	{
		Id = EntityID.Slime;
		Health = 10;
		HealthMax = 10;
		InventoryCapacity = 5;
	}
}