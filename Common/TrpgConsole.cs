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
	public static void Inventory(List<Item> invArray, int invCapacity)
	{
		Console.WriteLine($"Inventory [{invArray.Count}/{invCapacity}]:");

		foreach (var item in invArray)
		{
			Console.WriteLine($"\t- {item.Id}{(item.Amount > 1 ? " " + item.AmountString : "")} {item.Description}");
		}
	}
	public static void Inventory(List<Item> invArray, int invCapacity, string prefix)
	{
		Console.WriteLine($"{prefix}Inventory [{invArray.Count}/{invCapacity}]:");

		foreach (var item in invArray)
		{
			Console.WriteLine($"{prefix}\t- {item.Id}{(item.Amount > 1 ? " " + item.AmountString : "")} {item.Description}");
		}
	}
}