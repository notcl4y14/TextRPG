using Common.Items;
using Core;

namespace Content.Items;

class Pie : Food
{
	public Pie()
	{
		Id = ItemID.Pie;
		Name = "Pie";
		Description = "Heals 100 HP";
		HealPower = 100;
	}
}