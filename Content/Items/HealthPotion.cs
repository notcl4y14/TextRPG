using Common;
using Common.Items;
using Core;

namespace Content.Items;

class HealthPotion : Food
{
	public HealthPotion()
	{
		Id = ItemID.HealthPotion;
		Name = "HealthPotion";
		Description = "Why should it contain grass?";
		HealPower = 500;
		Dispense = ItemID.EmptyBottle;
	}

	public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.AddItem(ItemID.WaterBottle, 1);
		recipe.AddItem(ItemID.Apple, 1);
		recipe.AddItem(ItemID.Grass, 1);
		recipe.Register();
	}
}