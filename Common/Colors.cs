using Spectre.Console;

namespace Common;

class Colors
{
	public static Color CurrencyBronze = new Color(148, 102, 28);
	public static Color CurrencySilver = new Color(199, 199, 199);
	public static Color CurrencyGold = new Color(237, 207, 33);

	public static Color HealthOverMax = new Color(135, 23, 227);
	public static Color HealthMax = new Color(32, 161, 0);
	public static Color HealthHalf = new Color(237, 217, 0);
	public static Color HealthLow = new Color(191, 0, 0);

	public static Color ItemsNone = new Color(79, 79, 79);

	public static string GetHealthColor(int health, int healthMax)
	{
		int percentage = (int)((float)health / healthMax * 100);

		if (percentage > 100)
		{
			return "HealthOverMax";
		}
		else if (percentage >= 75)
		{
			return "HealthMax";
		}
		else if (percentage >= 50)
		{
			return "HealthHalf";
		}
		else
		{
			return "HealthLow";
		}
	}

	public static Color GetColorByName(string value)
	{
		switch (value)
		{
			case "CurrencyBronze":
				return CurrencyBronze;
			case "CurrencySilver":
				return CurrencySilver;
			case "CurrencyGold":
				return CurrencyGold;
			
			case "HealthOverMax":
				return HealthOverMax;
			case "HealthMax":
				return HealthMax;
			case "HealthHalf":
				return HealthHalf;
			case "HealthLow":
				return HealthLow;

			case "ItemsNone":
				return ItemsNone;
			
			default:
				return Color.DarkSlateGray1;
		}
	}

	public static string GetStringFromColor(Color color)
	{
		return $"rgb({color.R},{color.G},{color.B})";
	}

	public static string FormatString(string v)
	{
		string[] strings = [
			"CurrencyBronze", "CurrencySilver", "CurrencyGold",
			"HealthOverMax", "HealthMax", "HealthHalf", "HealthLow",
			"ItemsNone" ];
		string output = v;

		foreach (var str in strings)
		{
			Color color = GetColorByName(str);
			string rgb = GetStringFromColor(color);
			output = output.Replace($"[{str}]", $"[{rgb}]");
			// Console.WriteLine(output);
		}
		// Console.WriteLine(output);

		return output;
	}

	public static string ToAnsiString(string v)
	{
		// string output = v.Replace("[CurrencyBronze]", "[rgb(148,102,28)]");
		string output = FormatString(v);
		return output;
	}
}