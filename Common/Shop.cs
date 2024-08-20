using Core;

namespace Common;

class Shop
{
	public static Dictionary<ItemID, Currency> Items = [];

	public static void AddItem(ItemID itemID, Currency price)
	{
		Items[itemID] = price;
	}

	public static Item? BuyItem(ItemID itemID, ref Currency currency)
	{
		int currencySum = currency.GetSummary();
		int priceSum = Items[itemID].GetSummary();

		if (currencySum < priceSum)
		{
			return null;
		}

		Currency.Subtract(currency, Items[itemID]);
		currency.ConvertMoney();
		return ItemLibrary.GetFromID(itemID);
	}
}