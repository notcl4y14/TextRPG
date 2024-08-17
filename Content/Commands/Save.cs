using Core;

namespace Content.Commands;

class Save : Command
{
	public Save()
	{
		Name = "save";
		Alias = [];
	}

	public override void Run(string[] args, Entity entity)
	{
		if (args.Length < 2)
		{
			Console.WriteLine("save [playername] [filename]");
			return;
		}

		var playerName = args[0];
		var fileName = "./" + args[1];

		SaveState.Save(playerName, fileName, entity);
		Console.WriteLine("Saved successfully!");
	}
}