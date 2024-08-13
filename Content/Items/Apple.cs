using Core;

namespace Content.Items;

class Apple : Item
{
	public Apple()
	{
		Id = ItemID.Apple;
		Amount = 1;
		Description = "Heals 10 HP";
	}
	
	public override void Use(Entity user)
	{
		user.Heal(10);
	}
}