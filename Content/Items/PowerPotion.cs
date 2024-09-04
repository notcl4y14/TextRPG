using Common;
using Common.Items;
using Content.Buffs;
using Core;

namespace Content.Items;

class PowerPotion : Item
{
	public PowerPotion()
	{
		Id = ItemID.PowerPotion;
		Name = "PowerPotion";
		Description = "bottle of damage.";
	}

	public override void Eat(Entity user, Entity target)
	{
		user.RemoveItem(Id);
		user.AddItem(ItemLibrary.GetFromID(ItemID.EmptyBottle));

		TrpgConsole.WriteLine("Yum!");
		TrpgConsole.MarkupLine($"{target.Id} got [rgb(181,201,232)]Power[/] buff for 10 moves!");

		Power buff = new Power(50);
		buff.Moves = 10;
		target.AddBuff(buff);
	}

	public override void Use(Entity user, Entity target)
	{
		Eat(user, target);
	}

	public override void LoadRecipe()
	{
		var recipe = new CraftRecipe();
		recipe.ItemID = Id;
		recipe.AddItem(ItemID.WaterBottle, 1);
		recipe.AddItem(ItemID.Milk, 1);
		recipe.Register();
	}
}