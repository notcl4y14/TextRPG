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
		State = GameState.Fighting;
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
		// Currency currency = new Currency(Fighting.Bronze, Fighting.Silver, Fighting.Gold);
		Console.WriteLine("Victory!");
		Console.WriteLine(Currency.Present(Fighting.Currency));
		Currency.Add(Instance.Controller.Currency, Fighting.Currency);
		// Console.WriteLine(Currency.Present(currency));
		// Currency.Add((Instance.Controller as Player).Currency, currency);
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

		// Entity slime = EntityLibrary.GetFromID(EntityID.Slime);

		// if (slime == null)
		// {
		// 	Console.WriteLine("Content.Entities.Slime not found");
		// 	Exit();
		// 	return;
		// }

		Fighting.AddEnemy(new Slime());
		Fighting.AddEnemy(new Slime());
		Fighting.AddEnemy(new Slime());
		StartFight();
	}

	// Main
	public void Run()
	{
		IsRunning = true;

		while (IsRunning)
		{
			Console.Write(GetState() == GameState.Fighting ? Controller.HealthPercent + "% > " : "> ");
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
				// Player controller = Controller as Player;
				Console.WriteLine("Stats:");
				Console.WriteLine($"\tHealth: {Controller.HealthString} : {Controller.HealthPercent}%");
				Console.WriteLine($"\tInventory: {Controller.Inventory.Count}/{Controller.InventoryCapacity}");
				Console.WriteLine($"\tAttackSlot: {(Controller.AttackSlot != null ? Controller.AttackSlot.Id : "None")}");
				Console.WriteLine($"\tCurrency: Bronze: {Controller.Currency.Bronze}, Silver: {Controller.Currency.Silver}, Gold: {Controller.Currency.Gold}");
				// Console.WriteLine($"\tCurrency: Bronze: {controller.Currency.Bronze}, Silver: {controller.Currency.Silver}, Gold: {controller.Currency.Gold}");
				break;

			default:
				string name = command.Name.ToLower();
				string[] args = command.Arguments.ToArray();
				CommandLibrary.GetFromID(name)?.Run(args, ref Controller);
				break;
		}
	}
}