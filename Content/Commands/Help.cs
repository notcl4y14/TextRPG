using Core;
using Core.Cli;

namespace Content.Commands;

class Help : Command
{
	public static string[] Data = [
		"TextRPG",
		"https://github.com/notcl4y14/TextRPG",
		"",
		"Commands:",
		"\thelp|commands        - Outputs this list",
		"\tquit|exit            - Exits the game",
		"\tclear                - Clears the screen",
		"",
		"\tstats                - Outputs player's stats",
		"\tinventory|invent|inv - Outputs list of player's items",
		"\tequip                - Equips a given item to one of the slots",
		"\tuse                  - Interacts with the item from the inventory",
		"\teat                  - Eats the item, as long as it's edible",
		"\tcraft                - Outputs available items to craft and asks which one to make",
		"",
		"\tinfo                 - Outputs item's information",
		"\tshop|buy             - Outputs items in the shop and YOU CHOOSE AN ITEM",
		"\tpickup               - Gives a random item",
		"",
		"\tfight|encounter      - Starts a fight",
		"\tenemies              - Outputs list of enemies you're currently fighting",
		"\tattack               - Attacks the given enemy (The weapon should be equipped)",
		"\tdefense              - Defends and decreases given to you attack damage",
		"",
		"\tload - Loads save state",
		"\tsave - Saves current state"
	];

	public Help()
	{
		Name = "help";
		Alias = ["commands"];
	}

	public override void Run(string[] args, ref Entity user)
	{
		Console.WriteLine(String.Join("\n", Data));
	}
}