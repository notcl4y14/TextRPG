using Common;
using Core;
using Core.Cli;

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

		int index;

		try
		{
			index = Convert.ToInt32(args[0]);
		}
		catch
		{
			Console.WriteLine("Invalid input");
			return;
		}
		
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
			Console.WriteLine("Weapon Slot is empty");
			return;
		}

		user.Attack(enemy, user.AttackSlot, 0); // In case if AddDamage is greater than 0
		Fighting.MoveEnemies(user);
	}
}