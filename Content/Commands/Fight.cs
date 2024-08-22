using Common;
using Content.Entities;
using Core;
using Core.Cli;

namespace Content.Commands;

class Fight : Command
{
	public Fight()
	{
		Name = "fight";
		Alias = ["encounter"];
	}

	// private Enemy GetRandomEnemy()
	// {
	// 	// Random random = new Random();
	// 	// Enemy enemy = EnemyLibrary.GetFromID(entityID);

	// 	// TODO: Make an EnemyLibrary
	// 	// if (!(enemy is Enemy))
	// 	// {
	// 	// 	return GetRandomEnemy();
	// 	// }

	// 	// return enemy;
	// }

	public override void Run(string[] args, ref Entity user)
	{
		if (Game.GetState() == GameState.Fighting)
		{
			Console.WriteLine("You're already fighting!");
			return;
		}
		
		Random random = new Random();
		int enemyAmount = random.Next(1, 4);

		for (int i = 0; i < enemyAmount; i++)
		{
			Enemy _enemy = EnemyLibrary.GetRandom();
			Type enemyType = _enemy.GetType();
			Enemy enemy = (Enemy)Activator.CreateInstance(enemyType);
			Fighting.AddEnemy(enemy);
		}

		Game.StartFight();
	}
}