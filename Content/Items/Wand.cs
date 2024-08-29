using Common.Items;
using Core;

namespace Content.Items;

class Wand : Weapon
{
	public Wand()
	{
		Id = ItemID.Wand;
		Name = "Wand";
		Description = "Deals 10 damage";
		Damage = 10;
		CritDamage = 20;
		CritChance = 4;
	}
}