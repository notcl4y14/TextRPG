using Common;
using Content.Commands;
using Content.Entities;
using Content.Items;

namespace Core;

class Game
{
	bool IsRunning;
	Entity Controller;

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
				CommandLibrary.GetFromID(name)?.Run(args, Controller);
				break;
		}
	}
}