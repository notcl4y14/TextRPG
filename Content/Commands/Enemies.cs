using Common;
using Core;
using Core.Cli;

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
			int index = i + 1;

			if (enemy.IsDead)
			{
				// Console.WriteLine($"{index}: -");
				// Console.WriteLine($"{index}: {enemy.Id} (DEAD)");
				TrpgConsole.MarkupLine($"[ItemsNone]{index}: {enemy.Id} -------------------------[/]");
				continue;
			}

			string healthColor = Colors.GetHealthColor(enemy.Health, enemy.HealthMax);
			string buffs = "";

			foreach (var buff in enemy.Buffs)
			{
				buffs += buff.Icon;
			}

			TrpgConsole.MarkupLine($"{index}: {enemy.Id} ([{healthColor}]{enemy.Health}/{enemy.HealthMax}[/]) ({buffs})");
		}
	}
}