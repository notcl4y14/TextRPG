using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class DebugSetHealth : Command
{
	public DebugSetHealth()
	{
		Name = "debug_set_health";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		int health;
		
		if (args.Length > 0)
		{
			string input_health = args[0];
			health = Convert.ToInt32(input_health);
		}
		else
		{
			Console.WriteLine($"Current Health: {entity.Health}/{entity.HealthMax}");
			health = Convert.ToInt32(Log.AskInput("Health: "));
		}
		
		entity.Health = health;
	}
}