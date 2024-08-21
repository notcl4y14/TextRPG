using Common.Items;
using Core;

namespace Content.Items;

class Rock : Weapon
{
	public Rock()
	{
		Id = ItemID.Rock;
		Name = "Rock";
		Description = "That hurt.";
		Damage = 15;
		CritDamage = 50;
		CritChance = 5;
	}

	public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.Amount = 10;
		recipe.AddItem(ItemID.Boulder, 1);
		recipe.Register();
	}
}