using Common;
using Common.Items;
using Core;

namespace Content.Items;

class Water : Food
{
	public Water()
	{
		Id = ItemID.Water;
		Name = "Water";
		Description = "How are you holding it?";
		HealPower = 75;
	}
}