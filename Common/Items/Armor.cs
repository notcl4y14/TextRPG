using Core;

namespace Common.Items;

class Armor : Item
{
	public override ItemType Type { get; } = ItemType.Armor;
	public int Defense;

	public override void LoadStats()
	{
		Stats.Add("Defense", "" + Defense);
	}

	public override void Use(Entity user, Entity target)
	{
		return;
	}
}