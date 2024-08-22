using Common.Items;
using Core;

namespace Content.Items;

class Milk : Food
{
	public Milk()
	{
		Id = ItemID.Milk;
		Name = "Milk";
		Description = "Heals 25 HP";
		HealPower = 25;
	}
}