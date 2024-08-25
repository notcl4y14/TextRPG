using Core;

namespace Common.Items;

class Food : Item
{
	public override ItemType Type { get; } = ItemType.Food;
	public int HealPower;

	public override void LoadStats()
	{
		Stats.Add("Heals", "" + HealPower);
	}

	public override void Use(Entity user, Entity target)
	{
		Eat(user, target);
	}

	public override void Eat(Entity user, Entity target)
	{
		target.Heal((uint)HealPower);
		user.RemoveItem(Id);

		Console.WriteLine("Yum!");
		Console.WriteLine($"Got {HealPower} HP!");

		// if (user == target)
		// {
		// 	Console.WriteLine($"{user.Id} eats {Id}");
		// }
		// else
		// {
		// 	Console.WriteLine($"{user.Id} gives {target.Id} an {Id}");
		// }

		// Console.WriteLine($"{target.Id} heals {HealPower} HP!");
	}
}