using Common.Items;
using Core;

namespace Content.Items;

class PieSlice : Food
{
	public PieSlice()
	{
		Id = ItemID.PieSlice;
		Name = "PieSlice";
		Description = "Heals 10 HP";
		HealPower = 10;
	}

	public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.Amount = 10;
		recipe.AddItem(ItemID.Pie, 1);
		recipe.Register();
	}
}