using Common.Items;
using Core;

namespace Content.Items;

class Apple : Food
{
	public Apple()
	{
		Id = ItemID.Apple;
		Name = "Apple";
		Description = "Heals 10 HP";
		HealPower = 10;
	}

    // public override void Eat(Entity user, Entity target)
    // {
    //     target.Heal((uint)HealPower);
	// 	user.RemoveItem(Id);
    // }

    public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.AddItem(ItemID.Sword, 1);
		recipe.Register();
	}
}