using Content.Entities;
using Content.Items;

namespace Core;

class Game
{
	bool IsRunning;
	string Input;
	Entity Controller;

	public Game()
	{
		Input = "";
		IsRunning = false;
		Controller = new Player();
	}

	public void GetInput()
	{
		Input = Console.ReadLine() ?? "";
	}

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
			GetInput();
			CheckCommand();
		}
	}

	public void CheckCommand()
	{
		switch (Input.ToLower())
		{
			case "exit":
			case "quit":
				IsRunning = false;
				break;

			case "clear":
				Console.Clear();
				break;

			case "stats":
				Console.WriteLine("Stats");
				Console.WriteLine($"\tHealth: {Controller.HealthString} : {Controller.HealthPercent}%");
				Console.WriteLine($"\tInventory: {Controller.Inventory.Count}/{Controller.InventoryCapacity}");
				break;
			
			case "inv":
			case "invent":
			case "inventory":
				Console.WriteLine($"Inventory [{Controller.Inventory.Count}/{Controller.InventoryCapacity}]");

				foreach (var item in Controller.Inventory)
				{
					Console.WriteLine($"\t{item.Id}{(item.Amount > 1 ? " " + item.AmountString : "")} {item.Description}");
				}
				
				break;
			
			case "debug_add":
				{
					Console.Write("ItemID: ");
					string input = Console.ReadLine();
					
					ItemID itemID;
					try
					{
						itemID = (ItemID)Enum.Parse(typeof(ItemID), input);
					}
					catch(Exception e)
					{
						Console.WriteLine(e);
						return;
					}

					Item item;
					switch (itemID)
					{
						case ItemID.Apple:
							item = new Apple();
							break;
						default:
							return;
					}

					Controller.Add(item);
				}
				break;
		}
	}
}