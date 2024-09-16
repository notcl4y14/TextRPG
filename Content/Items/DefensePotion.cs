using Common;
using Common.Items;
using Content.Buffs;
using Core;

namespace Content.Items;

class DefensePotion : Item
{
	public DefensePotion()
	{
		Id = ItemID.DefensePotion;
		Name = "DefensePotion";
		Description = "bottle of defense.";
	}

	public override void Eat(Entity user, Entity target)
	{
		user.RemoveItem(Id);
		user.AddItem(ItemLibrary.GetFromID(ItemID.EmptyBottle));

		TrpgConsole.WriteLine("Yum!");
		TrpgConsole.MarkupLine($"{target.Id} got [rgb(223,230,242)]Defense[/] buff for 10 moves!");

		Defense buff = new Defense(50);
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
		recipe.AddItem(ItemID.Rock, 5);
		recipe.Register();
	}
}