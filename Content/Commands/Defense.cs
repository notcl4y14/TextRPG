using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class Defense : Command
{
	public Defense()
	{
		Name = "defense";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity user)
	{
		if (Game.GetState() != GameState.Fighting)
		{
			Console.WriteLine("You're not in a fight!");
			return;
		}

		user.Buffs.Add(new Content.Buffs.Defense(15));
		Fighting.MoveEnemies(user);
	}
}