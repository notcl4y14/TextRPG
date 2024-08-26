using Common;
using Core;

namespace Content.Items;

class EmptyBottle : Item
{
	public EmptyBottle()
	{
		Id = ItemID.EmptyBottle;
		Name = "EmptyBottle";
		Description = "bottle.";
	}

    public override void Use(Entity user, Entity target)
    {
        TrpgConsole.MarkupLine("You don't think there's much use for bottle outside of [bold]crafting[/]");
    }
}