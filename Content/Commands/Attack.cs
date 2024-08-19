using Core;

namespace Content.Commands;

class Attack : Command
{
	public Attack()
	{
		Name = "attack";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity user)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("attack [enemyIndex]");
			return;
		}

		if (Game.GetState() != GameState.Fighting)
		{
			if (Game.GetState() == GameState.GameOver)
			{
				Console.WriteLine("YOU FKIN DIED");
				return;
			}

			Console.WriteLine("You're not in a fight!");
			return;
		}

		int index = Convert.ToInt32(args[0]);
		Entity? enemy = Fighting.GetEnemy(index - 1);

		if (enemy == null)
		{
			Console.WriteLine($"There's no enemy at index {index}");
			return;
		}

		if (enemy.IsDead)
		{
			Console.WriteLine($"{enemy.Id} {index} is already dead");
			return;
		}

		if (user.AttackSlot == null)
		{
			Console.WriteLine("AttackSlot is empty");
			return;
		}

		user.Use(user.AttackSlot.Id, enemy);
		Fighting.MoveEnemies(user);
	}
}