using Core;
using Spectre.Console;

namespace Common;

class TrpgConsole
{
	// Output
	public static void Write(string v)
	{
		Console.Write(v);
	}

	public static void WriteLine(string v)
	{
		Console.WriteLine(v);
	}

	public static void Markup(string v)
	{
		AnsiConsole.Markup(Colors.ToAnsiString(v));
	}

	public static void MarkupLine(string v)
	{
		AnsiConsole.MarkupLine(Colors.ToAnsiString(v));
	}

	// Input
	public static string ReadLine()
	{
		return Console.ReadLine() ?? "";
	}

	public static string AskInput(string v)
	{
		Console.Write(v);
		return ReadLine();
	}

	public static bool AskConfirm(string v)
	{
		Console.Write(v);
		string input = ReadLine().ToUpper();

		if (input != "Y" && input != "N")
		{
			return AskConfirm(v);
		}

		return input == "Y";
	}

	// Misc.
	public static void Inventory(Inventory inventory)
	{
		int capacity = inventory.Capacity;
		List<Item> items = inventory.Items;
		Console.WriteLine($"Inventory [{items.Count}/{capacity}]:");

		foreach (var item in items)
		{
			string itemID = item.Id.ToString();
			string itemAmount = item.Amount > 1 ? " " + item.AmountString : "";
			Console.WriteLine($"\t- {item.Id}{itemAmount} {item.Description}");
		}
	}
	public static void Inventory(Inventory inventory, string prefix)
	{
		int capacity = inventory.Capacity;
		List<Item> items = inventory.Items;
		Console.WriteLine($"Inventory [{items.Count}/{capacity}]:");

		foreach (var item in items)
		{
			string itemID = item.Id.ToString();
			string itemAmount = item.Amount > 1 ? " " + item.AmountString : "";
			Console.WriteLine($"{prefix}- {item.Id}{itemAmount} {item.Description}");
		}
	}
}