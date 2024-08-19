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

	public void Move(Entity[] buddies, Entity opponent)
	{
		Console.WriteLine("Slime fights back!");
		Console.WriteLine("Slime deals 2 damage!");
		opponent.Hurt(2);
	}
}