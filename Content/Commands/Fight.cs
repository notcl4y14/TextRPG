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
			Fighting.AddEnemy(new Slime());
		}

		Game.StartFight();
	}
}