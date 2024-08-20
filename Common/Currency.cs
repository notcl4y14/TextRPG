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

	public int GetSummary () => Bronze + Silver * 100 + Gold * 100 * 100;

    public override string ToString() => $"Currency (Bronze: {Bronze}, Silver: {Silver}, Gold: {Gold})";

    public void ConvertMoney()
	{
		if (this.Bronze < 0)
		{
			this.Bronze = 100 - (this.Bronze * -1);
			this.Silver -= (this.Bronze / 100) + 1;
		}
		if (this.Silver < 0)
		{
			this.Silver = 100 - (this.Silver * -1);
			this.Gold -= (this.Silver / 100) + 1;
		}

		// https://stackoverflow.com/a/28008021/22146374
		if (this.Bronze >= 100)
		{
			this.Silver += (this.Bronze / 100);
			this.Bronze = this.Bronze % 100;
		}
		if (this.Silver >= 100)
		{
			this.Gold += (this.Silver / 100);
			this.Silver = this.Silver % 100;
		}
	}

	public static void Add(Currency destCurrency, Currency srcCurrency)
	{
		destCurrency.Bronze += srcCurrency.Bronze;
		destCurrency.Silver += srcCurrency.Silver;
		destCurrency.Gold += srcCurrency.Gold;
	}

	public static void Subtract(Currency destCurrency, Currency srcCurrency)
	{
		destCurrency.Bronze -= srcCurrency.Bronze;
		destCurrency.Silver -= srcCurrency.Silver;
		destCurrency.Gold -= srcCurrency.Gold;
	}

	public string Present()
	{
		string result = "";
		bool bronze = this.Bronze > 0;
		bool silver = this.Silver > 0;
		bool gold = this.Gold > 0;

		if (bronze)
		{
			result += $"Bronze: {this.Bronze}";
		}
		
		if (bronze && silver)
		{
			result += $", Silver: {this.Silver}";
		}
		else if (silver)
		{
			result += $"Silver: {this.Silver}";
		}

		if (silver && gold)
		{
			result += $", Gold: {this.Gold}";
		}
		else if (gold)
		{
			result += $"Gold: {this.Gold}";
		}

		return result;
	}
}