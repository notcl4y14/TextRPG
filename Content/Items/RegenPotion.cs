using Common;
using Common.Items;
using Content.Buffs;
using Core;

namespace Content.Items;

class RegenPotion : Item
{
	public RegenPotion()
	{
		Id = ItemID.RegenPotion;
		Name = "RegenPotion";
		Description = "bottle of regeneration.";
	}

	public override void Eat(Entity user, Entity target)
	{
		user.RemoveItem(Id);
		user.AddItem(ItemLibrary.GetFromID(ItemID.EmptyBottle));

		TrpgConsole.WriteLine("Yum!");
		TrpgConsole.MarkupLine($"{target.Id} got [rgb(167,242,61)]Regeneration[/] buff for 10 moves!");
		
		Regeneration buff = new Regeneration(10);
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
		recipe.AddItem(ItemID.EmptyBottle, 1);
		recipe.AddItem(ItemID.Water, 1);
		recipe.AddItem(ItemID.Apple, 1);
		recipe.Register();
	}
}