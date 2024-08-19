using Common.Items;
using Core;

namespace Content.Items;

class Boulder : Weapon
{
	public Boulder()
	{
		Id = ItemID.Boulder;
		Name = "Boulder";
		Description = "So, you wanna fight?";
		Damage = 100;
		CritDamage = 500;
		CritChance = 10;
	}
}