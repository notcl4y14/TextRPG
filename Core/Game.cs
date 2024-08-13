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
	public string AskInput(string str)
	{
		Console.Write(str);
		string input = Console.ReadLine() ?? "";
		return input;
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
			
			case "use":
				{
					// Show Inventory
					Console.WriteLine($"You have");

					foreach (var _item in Controller.Inventory)
					{
						Console.WriteLine($"\t{_item.Id}{(_item.Amount > 1 ? " " + _item.AmountString : "")}");
					}

					string input = AskInput("ItemID: ");

					// Get ItemID
					ItemID itemID = Item.GetIDFromString(input);

					if (itemID == ItemID.Null)
					{
						Console.WriteLine($"There's no {input}");
						return;
					}

					// Get Item
					Item? item = Controller.Inventory.Find(x => x.Id == itemID);

					if (item == null)
					{
						Console.WriteLine($"You don't have {input}");
						return;
					}

					item.Use(Controller);

					if (item.Amount == 0)
					{
						Controller.RemoveWholeItem(itemID);
					}
				}
				break;
			
			case "debug_add":
				{
					Console.Write("ItemID: ");
					string input = Console.ReadLine() ?? "";
					
					ItemID itemID = Item.GetIDFromString(input);
					Item? item = Item.CreateFromID(itemID);

					if (item == null)
					{
						return;
					}
					
					Controller.Add(item);
				}
				break;

			case "debug_set_health":
				{
					Console.WriteLine($"Current Health: {Controller.Health}/{Controller.HealthMax}");
					int input = Convert.ToInt32(AskInput("Health: "));
					Controller.Health = input;
				}
				break;
		}
	}
}