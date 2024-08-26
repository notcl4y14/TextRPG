using Common;
using Common.Items;
using Core;

namespace Content.Items;

class Grass : Item
{
	public Grass()
	{
		Id = ItemID.Grass;
		Name = "Grass";
		Description = "grass.";
	}

	public override void Use(Entity user, Entity target)
	{
		TrpgConsole.MarkupLine("You don't think there's much use for grass outside of [bold]crafting[/]");
	}
}