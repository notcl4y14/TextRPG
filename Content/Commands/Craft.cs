using Common;
using Core;
using Core.Cli;

namespace Content.Commands;

class Craft : Command
{
	public Craft()
	{
		Name = "craft";
		Alias = [];
	}

	private void ListItemRecipes(Entity entity)
	{
		foreach (var _recipe in CraftLibrary.Registers)
		{
			var recipe = _recipe.Value;

			if (!recipe.CheckIngredients(entity.Inventory))
			{
				continue;
			}

			Console.Write(_recipe.Key);

			if (recipe.Amount > 1)
			{
				Console.Write(" x" + recipe.Amount);
			}
			
			Console.Write(" (");

			// List all the ingredients
			foreach (var item in recipe.Ingredients)
			{
				Console.Write(item.Key);

				if (item.Value > 1)
				{
					Console.Write(" x" + item.Value);
				}

				if (item.Value != recipe.Ingredients.Last().Value)
				{
					Console.Write(", ");
				}
			}

			Console.Write(")\n");
		}
	}

	public override void Run(string[] args, ref Entity entity)
	{
		// List all the available items to craft
		ListItemRecipes(entity);

		// Choose Item
		string itemName = TrpgConsole.AskInput("ItemID: ");
		ItemID itemID = Item.GetIDFromString(itemName);

		if (itemID == ItemID.Null)
		{
			Console.WriteLine($"There's no ItemID {itemName}");
			return;
		}

		CraftRecipe? recipe = CraftLibrary.GetRecipe(itemID);

		if (recipe == null)
		{
			Console.WriteLine($"{itemID} does not have a recipe");
			return;
		}

		if (!recipe.CheckIngredients(entity.Inventory))
		{
			Console.WriteLine($"You don't have enough resources to craft {itemID}");
			return;
		}

		if (entity.IsInventoryFull())
		{
			Console.WriteLine($"You don't have enough space in your inventory!");
			return;
		}

		entity.AddItem( ItemLibrary.GetFromID(recipe.ItemID), recipe.Amount );

		// Remove Items from the inventory
		foreach (var item in entity.Inventory)
		{
			if (recipe.Ingredients.ContainsKey(item.Id))
			{
				// if (item.Amount < recipe.Items[item.Id])
				// {
				// 	break;
				// }

				item.Amount -= recipe.Ingredients[item.Id];

				if (item.Amount < 1)
				{
					entity.RemoveWholeItem(item.Id);
				}

				break;
			}
		}
	}
}