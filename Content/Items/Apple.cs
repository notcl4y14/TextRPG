using Common.Items;
using Core;

namespace Content.Items;

class Apple : Food
{
	public Apple()
	{
		Id = ItemID.Apple;
		Amount = 1;
		Description = "Heals 10 HP";
		HealPower = 10;
	}
}