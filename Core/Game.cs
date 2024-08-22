using Common;
using Content.Commands;
using Content.Entities;
using Content.Items;

namespace Core;

class Game
{
	private static Game Instance;
	bool IsRunning;
	GameState State;
	Entity Controller;

	public Game()
	{
		IsRunning = false;
		State = GameState.Play;
		Controller = new Player();
		Instance = this;
	}

	// Misc.
	public static void GameOver()
	{
		SetState(GameState.GameOver);
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

	public static GameState GetState()
	{
		return Instance.State;
	}

	public static void SetState(GameState state)
	{
		Instance.State = state;
	}

	public static void StartFight()
	{
		SetState(GameState.Fighting);

		Console.WriteLine(Fighting.PresentEncounter());
	}

	public static void EndFight()
	{
		SetState(GameState.Play);
		Console.WriteLine("Victory!");
		Console.WriteLine(Fighting.Currency.Present());
		Currency.Add(Instance.Controller.Cash, Fighting.Currency);
		Instance.Controller.Cash.ConvertMoney();
	}

	public static void Exit()
	{
		Environment.Exit(0);
	}

	// Init
	public void Load()
	{
		LibraryLoader.LoadCommands();
		LibraryLoader.LoadEntities();
		LibraryLoader.LoadItems();

		Common.Shop.AddItem(ItemID.Sword, new Currency(silver: 10));
		Common.Shop.AddItem(ItemID.Boulder, new Currency(bronze: 50));
		Common.Shop.AddItem(ItemID.Apple, new Currency(silver: 1));
	}

	// Main
	public void Run()
	{
		IsRunning = true;

		while (IsRunning)
		{
			Console.Write(Controller.HealthString + " > ");
			string input = Log.ReadLine();
			CommandInput command = CommandInput.FromString(input);
			CheckCommand(command);
		}
	}

	public void CheckCommand(CommandInput commandInput)
	{
		switch (commandInput.Name.ToLower())
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
				Console.WriteLine($"\tInventory: {Controller.InventoryString}");
				Console.WriteLine($"\tCash: {Controller.CashString}");
				Console.WriteLine($"\tWeapon Slot: {Controller.AttackSlotString}");
				break;

			default:
				string name = commandInput.Name.ToLower();
				string[] args = commandInput.Arguments.ToArray();
				Command command = CommandLibrary.GetFromID(name);

				if (command == null)
				{
					Console.WriteLine($"Unknown command {name}");
					break;
				}

				command.Run(args, ref Controller);
				break;
		}
	}
}