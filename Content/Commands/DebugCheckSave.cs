using Core;

namespace Content.Commands;

class DebugCheckSave : Command
{
	public DebugCheckSave()
	{
		Name = "debug_check_save";
		Alias = [];
	}

	public override void Run(string[] args, ref Entity entity)
	{
		Console.WriteLine("IsOnSave: " + SaveState.IsOnSave);
		Console.WriteLine("PlayerName: " + SaveState.PlayerName);
		Console.WriteLine("FileName: " + SaveState.FileName);
	}
}