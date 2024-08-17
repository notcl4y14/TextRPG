using Core;

namespace Content.Commands;

class Save : Command
{
	public Save()
	{
		Name = "save";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		if (args.Length < 2 && !SaveState.IsOnSave)
		{
			Console.WriteLine("save [playername] [filename]");
			return;
		}

		var playerName = args.Length < 2 ? SaveState.PlayerName : args[0];
		var fileName = args.Length < 2 ? SaveState.FileName : "./" + args[1];

		SaveState.Save(playerName, fileName, entity);
		Console.WriteLine("Saved successfully!");
	}
}