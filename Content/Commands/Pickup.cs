using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class Pickup : Command
{
	public Pickup()
	{
		Name = "pickup";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity user)
	{
		Random random = new Random();
		int value = random.Next(0, 2);

		string item = "";

		switch (value)
		{
			case 0:
				item = "Grass";
				user.AddItem( ItemLibrary.GetFromID(ItemID.Grass) );
				break;
			case 1:
				item = "Rock";
				user.AddItem( ItemLibrary.GetFromID(ItemID.Rock) );
				break;
		}

		TrpgConsole.MarkupLine($"Found [yellow]{item}[/]!");
	}
}