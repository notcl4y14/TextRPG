using Spectre.Console;

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

	public static void Multiply(Currency destCurrency, Currency srcCurrency)
	{
		destCurrency.Bronze *= srcCurrency.Bronze;
		destCurrency.Silver *= srcCurrency.Silver;
		destCurrency.Gold *= srcCurrency.Gold;
	}
	
	public static Currency Multiplied(Currency srcCurrency, int mul)
	{
		Currency destCurrency = new Currency(srcCurrency.Bronze, srcCurrency.Silver, srcCurrency.Gold);
		destCurrency.Bronze *= mul;
		destCurrency.Silver *= mul;
		destCurrency.Gold *= mul;
		return destCurrency;
	}

	public string Present()
	{
		string result = "";
		bool bronze = this.Bronze > 0;
		bool silver = this.Silver > 0;
		bool gold = this.Gold > 0;

		// AnsiConsole.MarkupLine(Colors.ToAnsiString("[CurrencyBronze]BRONZE[/]"));

		if (bronze)
		{
			result += $"Bronze: [CurrencyBronze]{this.Bronze}[/]";
		}
		
		if (bronze && silver)
		{
			result += $", Silver: [CurrencySilver]{this.Silver}[/]";
		}
		else if (silver)
		{
			result += $"Silver: [CurrencySilver]{this.Silver}[/]";
		}

		if ((bronze && gold) || (silver && gold))
		{
			result += $", Gold: [CurrencyGold]{this.Gold}[/]";
		}
		else if (gold)
		{
			result += $"Gold: [CurrencyGold]{this.Gold}[/]";
		}

		return result;
	}
}