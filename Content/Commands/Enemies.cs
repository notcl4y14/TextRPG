using Core;

namespace Content.Commands;

class Enemies : Command
{
	public Enemies()
	{
		Name = "enemies";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity user)
	{
		Entity[] enemies = Fighting.Enemies.ToArray();
		for (int i = 0; i < enemies.Length; i++)
		{
			Entity enemy = enemies[i];
			Console.WriteLine($"{i}: {enemy.Id} ({enemy.Health}/{enemy.HealthMax})");
		}
	}
}