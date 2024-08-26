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
			for (int i = 0; i < recipe.Ingredients.Count; i++)
			{
				ItemID item = recipe.Ingredients.Keys.ElementAt(i);
				int amount = recipe.Ingredients.Values.ElementAt(i);

				Console.Write(item);

				if (amount > 1)
				{
					Console.Write(" x" + amount);
				}

				if (i != recipe.Ingredients.Count - 1)
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
		List<ItemID> list = [];

		foreach (var item in entity.Inventory)
		{
			if (recipe.Ingredients.ContainsKey(item.Id))
			{
				list.Add(item.Id);
			}
		}

		foreach (var itemId in list)
		{
			entity.RemoveItem(itemId, recipe.Ingredients[itemId]);
		}
	}
}