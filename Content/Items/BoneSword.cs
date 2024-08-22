using Common.Items;
using Core;

namespace Content.Items;

class BoneSword : Weapon
{
	public BoneSword()
	{
		Id = ItemID.BoneSword;
		Name = "BoneSword";
		Description = "Deals 25 Damage";
		Damage = 25;
		CritDamage = 50;
		CritChance = 4;
	}
}