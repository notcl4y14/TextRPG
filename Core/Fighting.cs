using Content.Entities;

namespace Core;

class Fighting
{
	public static List<Entity> Enemies = [];

	// Enemies
	public static void AddEnemy(Entity enemy)
	{
		Enemies.Add(enemy);
	}

	public static void RemoveEnemy(int index)
	{
		Enemies.RemoveAt(index);
	}

	public static void ClearEnemies()
	{
		Enemies = [];
	}

	public static Entity? GetEnemy(int index)
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

	public static Entity GetEnemyNs(int index)
	{
		return Enemies[index];
	}

	public static string PresentEncounter()
	{
		string result = "";

		for (int i = 0; i < Enemies.Count; i++)
		{
			Entity enemy = Enemies[i];

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
		if (Enemies.Count == 0)
		{
			ClearEnemies();
			Game.EndFight();
		}
	}

	public static void MoveEnemies(Entity player)
	{
		for (int i = 0; i < Enemies.Count; i++)
		{
			Entity enemy = GetEnemyNs(i);

			if (enemy.IsDead)
			{
				Enemies[i] = null;
				continue;
			}

			(enemy as Slime).Move(Enemies.ToArray(), player);
		}

		for (int i = 0; i < Enemies.Count; i++)
		{
			Entity enemy = GetEnemyNs(i);
			
			if (enemy == null)
			{
				RemoveEnemy(i);
				CheckEnemies();
			}
		}
	}
}