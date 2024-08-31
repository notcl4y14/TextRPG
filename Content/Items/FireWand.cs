using Common.Items;
using Content.Buffs;
using Core;

namespace Content.Items;

class FireWand : Weapon
{
	public FireWand()
	{
		Id = ItemID.FireWand;
		Name = "FireWand";
		Description = "Deals 10 damage; Gives Burn debuff for 4 moves";
		Damage = 10;
		CritDamage = 20;
		CritChance = 4;
	}

	public override void Use(Entity user, Entity target)
	{
		base.Use(user, target);
		Buff buff = new Burn(2);
		buff.Moves = 4;
		target.AddBuff(buff);
	}

	public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.AddItem(ItemID.Wand, 1);
		recipe.AddItem(ItemID.Fire, 1);
		recipe.Register();
	}
}