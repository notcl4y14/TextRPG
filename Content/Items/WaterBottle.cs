using Common;
using Common.Items;
using Core;

namespace Content.Items;

class WaterBottle : Food
{
	public WaterBottle()
	{
		Id = ItemID.WaterBottle;
		Name = "WaterBottle";
		Description = "bottle of water.";
		HealPower = 75;
		Dispense = ItemID.EmptyBottle;
	}

	public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.AddItem(ItemID.EmptyBottle, 1);
		recipe.AddItem(ItemID.Water, 1);
		recipe.Register();
	}
}