using Common;

namespace Core;

class CraftRecipe
{
	public Dictionary<ItemID, int> Ingredients;
	public ItemID ItemID;
	public int Amount;

	public CraftRecipe()
	{
		Ingredients = [];
		Amount = 1;
	}

	private bool InventoryHasItem(List<Item> inventory, ItemID itemID, int amount)
	{
		foreach (var item in inventory)
		{
			if (item.Id == itemID)
			{
				if (item.Amount < amount)
				{
					return false;
				}

				return true;
			}
		}

		return false;
	}

	public bool CheckIngredients(List<Item> inventory)
	{
		foreach (var ingredient in Ingredients)
		{
			if (!InventoryHasItem(inventory, ingredient.Key, ingredient.Value))
			{
				return false;
			}
		}

		return true;
	}

	public void AddItem(ItemID itemID, int amount = 1)
	{
		Ingredients[itemID] = amount;
	}

	public void Register()
	{
		CraftLibrary.Register(ItemID, this);
	}
}