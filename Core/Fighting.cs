using Common;

namespace Core;

class Fighting
{
	public static List<Enemy> Enemies = [];
	public static List<int> AlreadyDeadEnemies = [];
	public static Currency Currency = new Currency(bronze: 0, silver: 0, gold: 0);
	// public static int Bronze = 0;
	// public static int Silver = 0;
	// public static int Gold = 0;

	// Enemies
	public static void AddEnemy(Enemy enemy)
	{
		enemy.Index = Enemies.Count + 1;
		Enemies.Add(enemy);
	}

	public static void RemoveEnemy(int index)
	{
		Enemies[index] = null;
	}

	public static void Clear()
	{
		Enemies = [];
		AlreadyDeadEnemies = [];
		Currency = new Currency(bronze: 0, silver: 0, gold: 0);
		// Bronze = 0;
		// Silver = 0;
		// Gold = 0;
	}

	public static Enemy? GetEnemy(int index)
	{
		try
		{
			return Enemies[index];
		}
		catch
		{
			return null;
		}
	}

	public static Enemy GetEnemyNs(int index)
	{
		return Enemies[index];
	}

	public static string PresentEncounter()
	{
		string result = "";

		for (int i = 0; i < Enemies.Count; i++)
		{
			Enemy enemy = Enemies[i];

			if (enemy == null)
			{
				continue;
			}

			result += enemy.Id;

			if (i != Enemies.Count - 1)
			{
				result += ", ";
			}
		}

		result += Enemies.Count == 1 ? " is" : " are";
		result += " blocking your path!";

		return result;
	}

	public static void CheckEnemies()
	{
		Enemy[] left = Enemies.FindAll(x => !x.IsDead).ToArray();

		if (left.Length == 0)
		{
			Game.EndFight();
			Clear();
		}
	}

	public static void MoveEnemies(Entity player)
	{
		for (int i = 0; i < Enemies.Count; i++)
		{
			Enemy enemy = GetEnemyNs(i);

			if (enemy.IsDead && !AlreadyDeadEnemies.Contains(i))
			{
				Currency.Add(Currency, enemy.Currency);
				// Console.WriteLine(enemy.Currency);
				// Console.WriteLine(Currency);
				CheckEnemies();
			}

			if (enemy.IsDead)
			{
				// Bronze += enemy.Bronze;
				// Silver += enemy.Silver;
				// Gold += enemy.Gold;
				AlreadyDeadEnemies.Add(i);
				continue;
			}

			enemy.Move(Enemies.ToArray(), player);
		}
	}
}