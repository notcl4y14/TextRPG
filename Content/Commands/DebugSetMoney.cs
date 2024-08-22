using Core;

namespace Content.Commands;

class DebugSetMoney : Command
{
	public DebugSetMoney()
	{
		Name = "debug_set_money";
		Alias = [];
	}

	private void SetOneAmount(ref Entity user, string type, int amount)
	{
		switch (type)
		{
			case "bronze":
				user.Cash.Bronze = amount;
				Console.WriteLine($"Successfully set Bronze to {amount}!");
				break;
			case "silver":
				user.Cash.Silver = amount;
				Console.WriteLine($"Successfully set Silver to {amount}!");
				break;
			case "gold":
				user.Cash.Gold = amount;
				Console.WriteLine($"Successfully set Gold to {amount}!");
				break;
			default:
				Console.WriteLine($"Unknown Money type {type}");
				break;
		}
	}

	private void SetAllAmount(ref Entity user, int bronze, int silver, int gold)
	{
		user.Cash.Bronze = bronze;
		user.Cash.Silver = silver;
		user.Cash.Gold = gold;
		Console.WriteLine("Successfully set new values!");
	}

	public override void Run(string[] args, ref Entity user)
	{
		if (args.Length < 2)
		{
			Console.WriteLine("debug_set_money [bronze|silver|gold] [amount]");
			Console.WriteLine("debug_set_money [bronze amount] [silver amount] [gold amount]");
		}

		if (args.Length == 2)
		{
			int amount;

			try
			{
				amount = Convert.ToInt32(args[1]);
			}
			catch
			{
				Console.WriteLine("Invalid input");
				return;
			}
			
			SetOneAmount(ref user, args[0], amount);
		}
		else if (args.Length == 3)
		{
			int bronze;
			int silver;
			int gold;

			try
			{
				bronze = Convert.ToInt32(args[0]);
				silver = Convert.ToInt32(args[1]);
				gold = Convert.ToInt32(args[2]);
			}
			catch
			{
				Console.WriteLine("Invalid input");
				return;
			}

			SetAllAmount(ref user, bronze, silver, gold);
		}
	}
}