using Common;
using Content.Commands;
using Content.Entities;
using Content.Items;

namespace Core;

class Game
{
	private static Game Instance;
	bool IsRunning;
	Entity Controller;

	public Game()
	{
		IsRunning = false;
		Controller = new Player();
		Instance = this;
	}

	// Misc.
	public static void GameOver()
	{
		Console.WriteLine("Game Over");

		if (!SaveState.IsOnSave)
		{
			bool chooseSave = Log.AskConfirm("Choose another save? (Y/n)\n");

			if (chooseSave)
			{
				string fileName = Log.AskInput("Filename: ");
				Instance.Controller = SaveState.Load(fileName);
			}
			else
			{
				Exit();
			}

			return;
		}

		Entity entity = SaveState.Load(SaveState.FileName);
		
		Console.WriteLine("Continue from last save? (Y/n)");
		Console.WriteLine($"\n\tHealth: {entity.Health}/{entity.HealthMax}");
		Log.Inventory(Instance.Controller.Inventory, Instance.Controller.InventoryCapacity, "\t");

		Console.WriteLine("\nNOTE: If you choose \"n\", the game will quit");

		bool continueSave = Log.AskConfirm("Continue from last save? (Y/n)\n");

		if (continueSave)
		{
			Instance.Controller = SaveState.Load(SaveState.FileName);
		}
		else
		{
			Exit();
		}
	}

	public static void Exit()
	{
		Environment.Exit(0);
	}

	// Init
	public void Load()
	{
		ItemLibrary.Register(ItemID.Apple, new Apple().Load());
		ItemLibrary.Register(ItemID.Sword, new Sword().Load());

		CommandLibrary.Register("inventory", new Inventory().Load());
		CommandLibrary.Register("use", new Use().Load());
		CommandLibrary.Register("info", new Info().Load());
		CommandLibrary.Register("craft", new Craft().Load());
		CommandLibrary.Register("save", new Save().Load());
		CommandLibrary.Register("load", new Load().Load());
		CommandLibrary.Register("debug_add", new DebugAdd().Load());
		CommandLibrary.Register("debug_set_health", new DebugSetHealth().Load());
		CommandLibrary.Register("debug_list_items", new DebugListItems().Load());
		CommandLibrary.Register("debug_check_save", new DebugCheckSave().Load());
	}

	// Main
	public void Run()
	{
		IsRunning = true;

		Console.WriteLine(Controller);
		Console.WriteLine(Controller.Id);
		Console.WriteLine(Controller.HealthString);
		Console.WriteLine(Controller.HealthPercent);

		while (IsRunning)
		{
			Console.Write("> ");
			string input = Log.ReadLine();
			CommandInput command = CommandInput.FromString(input);
			CheckCommand(command);
		}
	}

	public void CheckCommand(CommandInput command)
	{
		switch (command.Name.ToLower())
		{
			case "exit":
			case "quit":
				IsRunning = false;
				break;

			case "clear":
				Console.Clear();
				break;

			case "stats":
				Console.WriteLine("Stats:");
				Console.WriteLine($"\tHealth: {Controller.HealthString} : {Controller.HealthPercent}%");
				Console.WriteLine($"\tInventory: {Controller.Inventory.Count}/{Controller.InventoryCapacity}");
				break;

			default:
				string name = command.Name.ToLower();
				string[] args = command.Arguments.ToArray();
				CommandLibrary.GetFromID(name)?.Run(args, ref Controller);
				break;
		}
	}
}