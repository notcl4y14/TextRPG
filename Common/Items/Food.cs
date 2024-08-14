using Core;

namespace Common.Items;

class Food : Item
{
	public int HealPower;
	
    public override void Use(Entity user, Entity target)
    {
		RemoveOne();
		target.Heal((uint)HealPower);
	}
}