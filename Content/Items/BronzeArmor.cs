using Common.Items;
using Core;

namespace Content.Items;

class BronzeArmor : Armor
{
	public BronzeArmor()
	{
		Id = ItemID.BronzeArmor;
		Name = "BronzeArmor";
		Description = "Defense 10";
		Defense = 10;
	}
}