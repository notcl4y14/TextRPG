using Common.Items;
using Core;

namespace Content.Items;

class Sword : Weapon
{
	public Sword()
	{
		Id = ItemID.Sword;
		Amount = 1;
		Description = "Deals 10 Damage";
		Damage = 10;
		CritDamage = 20;
		CritChance = 4;
	}
}