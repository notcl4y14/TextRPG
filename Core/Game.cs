using Common;
using Content.Commands;
using Content.Entities;
using Content.Items;
using Core.Cli;
using Spectre.Console;

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
		TrpgConsole.WriteLine("Game Over");

		bool quit = SaveState.IsOnSave
			? GameOver_SaveState()
			: GameOver_NoSaveState();
		
		if (quit)
		{
			Exit();
		}
	}

	public static bool GameOver_NoSaveState()
	{
		bool chooseSave = TrpgConsole.AskConfirm("Choose another save? (Y/n)\n");

		if (chooseSave)
		{
			string fileName = TrpgConsole.AskInput("Filename: ");
			Instance.Controller = SaveState.Load(fileName);

			return false;
		}

		return true;
	}

	public static bool GameOver_SaveState()
	{
		Entity entity = SaveState.Load(SaveState.FileName);

		TrpgConsole.WriteLine("Continue from last save? (Y/n)");
		TrpgConsole.WriteLine($"\n\tHealth: {entity.Health}/{entity.HealthMax}");
		TrpgConsole.Inventory(Instance.Controller.Inventory, "\t");

		TrpgConsole.WriteLine("\nNOTE: If you choose \"n\", the game will quit");

		bool continueSave = TrpgConsole.AskConfirm("Continue from last save? (Y/n)\n");

		if (continueSave)
		{
			Instance.Controller = SaveState.Load(SaveState.FileName);
			return false;
		}

		return true;
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
		TrpgConsole.WriteLine(Fighting.PresentEncounter());
		SetState(GameState.Fighting);
	}

	public static void EndFight()
	{
		TrpgConsole.WriteLine("\n==== Victory! ====\n");
		TrpgConsole.MarkupLine(Fighting.Currency.Present());

		SetState(GameState.Play);

		foreach (var item in Fighting.Items)
		{
			Instance.Controller.AddItem(item);
			TrpgConsole.WriteLine($"You got {item.Id}!");
		}
		
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
		Common.Shop.AddItem(ItemID.Pie, new Currency(silver: 75));
		Common.Shop.AddItem(ItemID.BronzeArmor, new Currency(bronze: 10));
		Common.Shop.AddItem(ItemID.IronArmor, new Currency(silver: 50));
		Common.Shop.AddItem(ItemID.EmptyBottle, new Currency(bronze: 10));
		Common.Shop.AddItem(ItemID.WaterBottle, new Currency(bronze: 25));
	}

	// Main
	public void Run()
	{
		IsRunning = true;

		while (IsRunning)
		{
			string healthColor = Colors.GetHealthColor(Controller.Health, Controller.HealthMax);
			string health = $"[{healthColor}]{Controller.HealthString}[/]";
			string buffs = "";

			foreach (var buff in Controller.Buffs)
			{
				buffs += buff.Icon;
			}
			
			TrpgConsole.Markup($"{health} {buffs}> ");

			string input = TrpgConsole.ReadLine();
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
				string healthColor = Colors.GetHealthColor(Controller.Health, Controller.HealthMax);
				TrpgConsole.WriteLine("Stats:");
				TrpgConsole.MarkupLine($"\tHealth: [{healthColor}]{Controller.HealthString}[/] : [{healthColor}]{Controller.HealthPercent}%[/]");
				TrpgConsole.WriteLine($"\tInventory: {Controller.InvCapacityString}");
				TrpgConsole.MarkupLine($"\tCash: {Controller.CashString}");
				TrpgConsole.WriteLine($"\tDefense: {Controller.Defense}");
				TrpgConsole.MarkupLine($"\tWeapon Slot: {Controller.AttackSlotString}");
				TrpgConsole.MarkupLine($"\tArmor Slot: {Controller.ArmorSlotString}");

				var hasBuffs = Controller.Buffs.Count > 0;
				TrpgConsole.Write($"\tBuffs:");
				if (hasBuffs)
				{
					TrpgConsole.Write("\n");
					TrpgConsole.Buffs(Controller.Buffs, "\t\t");
				}
				else
				{
					TrpgConsole.MarkupLine(" [ItemsNone]No buffs[/]");
				}
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