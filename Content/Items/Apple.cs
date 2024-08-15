using Common.Items;
using Core;

namespace Content.Items;

class Apple : Food
{
	public Apple()
	{
		Id = ItemID.Apple;
		Name = "Apple";
		Description = "Heals 10 HP";
		HealPower = 10;
	}
}