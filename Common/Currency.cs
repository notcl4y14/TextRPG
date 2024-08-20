namespace Common;

class Currency
{
	public int Bronze;
	public int Silver;
	public int Gold;

	public Currency(int bronze = 0, int silver = 0, int gold = 0)
	{
		Bronze = bronze;
		Silver = silver;
		Gold = gold;
	}

    public override string ToString()
    {
        return $"Currency (Bronze: {Bronze}, Silver: {Silver}, Gold: {Gold})";
    }

    public static void ConvertMoney(Currency currency)
	{
		if (currency.Bronze >= 100)
		{
			int part = (int)(currency.Bronze / 100);
			currency.Bronze -= part * 100;
			currency.Silver += part;
		}
		if (currency.Silver >= 100)
		{
			int part = (int)(currency.Silver / 100);
			currency.Silver -= part * 100;
			currency.Gold += part;
		}
	}

	public static void Add(Currency destCurrency, Currency srcCurrency)
	{
		destCurrency.Bronze += srcCurrency.Bronze;
		destCurrency.Silver += srcCurrency.Silver;
		destCurrency.Gold += srcCurrency.Gold;
	}

	public static string Present(Currency currency)
	{
		string result = "";
		bool bronze = currency.Bronze > 0;
		bool silver = currency.Silver > 0;
		bool gold = currency.Gold > 0;

		if (bronze)
		{
			result += $"Bronze: {currency.Bronze}";
		}
		
		if (bronze && silver)
		{
			result += $", Silver: {currency.Silver}";
		}
		else if (silver)
		{
			result += $"Silver: {currency.Silver}";
		}

		if (silver && gold)
		{
			result += $", Gold: {currency.Gold}";
		}
		else if (gold)
		{
			result += $"Gold: {currency.Gold}";
		}

		return result;
	}
}