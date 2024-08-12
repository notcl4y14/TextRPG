using Content.Entities;

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
				break;
		}
	}
}