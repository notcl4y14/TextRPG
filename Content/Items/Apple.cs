using Core;

namespace Content.Items;

class Apple : Item
{
	public Apple()
	{
		Id = ItemID.Apple;
		Amount = 1;
		Description = "Heals 10 HP";
		DestroyOnUse = true;
	}
	
	public override void Use(Entity user, Entity target)
	{
		base.Use(user, target);
		target.Heal(10);
	}
}