using Common;
using Content.Commands;
using Content.Entities;
using Content.Items;

namespace Core;

class Game
{
	bool IsRunning;
	private static Entity Controller;

	public Game()
	{
		IsRunning = false;
		Controller = new Player();
	}

	// Input
	public string GetInput()
	{
		return Console.ReadLine() ?? "";
	}
	
	public static string AskInput(string str)
	{
		Console.Write(str);
		string input = Console.ReadLine() ?? "";
		return input;
	}

	// Misc.
	public static void GameOver()
	{
		Console.WriteLine("Game Over");

		if (!SaveState.IsOnSave)
		{
			Console.WriteLine("You haven't made any save");
			string _input = AskConfirm("Choose another save? (Y/n)\n");

			if (_input.ToUpper() == "Y")
			{
				string fileName = AskInput("Filename: ");
				Controller = SaveState.Load(fileName);
			}
			else if (_input.ToUpper() == "N")
			{
				Environment.Exit(0);
			}

			return;
		}

		Entity entity = SaveState.Load(SaveState.FileName);
		
		Console.WriteLine("Continue from last save? (Y/n)");
		Console.WriteLine($"\n\tHealth: {entity.Health}/{entity.HealthMax}");
		Console.WriteLine($"\tInventory: [{entity.Inventory.Count}/{entity.InventoryCapacity}]");

		foreach (var item in entity.Inventory)
		{
			Console.WriteLine($"\t\t- {item.Id}{(item.Amount > 1 ? " " + item.AmountString : "")}");
		}

		Console.WriteLine("\nNOTE: If you choose \"n\", the game will quit");

		string input = AskConfirm("Continue from last save? (Y/n)\n");

		if (input.ToUpper() == "Y")
		{
			Controller = SaveState.Load(SaveState.FileName);
		}
		else if (input.ToUpper() == "N")
		{
			Environment.Exit(0);
		}
	}

	public static string AskConfirm(string str)
	{
		Console.Write(str);
		string input = Console.ReadLine();

		if (input.ToUpper() != "Y" && input.ToUpper() != "N")
		{
			return AskConfirm(str);
		}

		return input;
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
			string input = GetInput();
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