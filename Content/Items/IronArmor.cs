using Common.Items;
using Core;

namespace Content.Items;

class IronArmor : Armor
{
	public IronArmor()
	{
		Id = ItemID.IronArmor;
		Name = "IronArmor";
		Description = "Defense 50";
		Defense = 50;
	}
}