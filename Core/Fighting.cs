using Content.Entities;

namespace Core;

class Fighting
{
	public static List<Enemy> Enemies = [];

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

	public static void ClearEnemies()
	{
		Enemies = [];
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
			ClearEnemies();
			Game.EndFight();
		}
	}

	public static void MoveEnemies(Entity player)
	{
		for (int i = 0; i < Enemies.Count; i++)
		{
			Enemy enemy = GetEnemyNs(i);

			if (enemy.IsDead)
			{
				CheckEnemies();
				continue;
			}

			enemy.Move(Enemies.ToArray(), player);
		}
	}
}