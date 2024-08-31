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
}