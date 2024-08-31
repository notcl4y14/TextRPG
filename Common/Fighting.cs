using Core;

namespace Common;

class Fighting
{
	public static List<Enemy> Enemies = [];
	public static List<int> AlreadyDeadEnemies = [];
	public static Currency Currency = new Currency(bronze: 0, silver: 0, gold: 0);
	public static List<Item> Items = [];

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
		Items = [];
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
		foreach (var buff in player.Buffs)
		{
			buff.Step(player);
		}

		for (int i = 0; i < Enemies.Count; i++)
		{
			Enemy enemy = GetEnemyNs(i);

			if (enemy.IsDead && !AlreadyDeadEnemies.Contains(i))
			{
				Currency.Add(Currency, enemy.Cash);

				Random random = new Random();

				foreach (var entry in enemy.Items)
				{
					int value = random.Next(entry.Value);

					if (value == 1)
					{
						Items.Add(ItemLibrary.GetFromID(entry.Key));
					}
				}
				
				AlreadyDeadEnemies.Add(i);
				CheckEnemies();
				continue;
			}

			if (enemy.IsDead)
			{
				continue;
			}

			// Apply buffs
			enemy.Buffs.ForEach(buff => buff.Step(enemy));

			foreach (var buff in enemy.Buffs)
			{
				if (buff.Moves < 1)
				{
					buff.OnFinish(enemy);
				}
			}

			enemy.Buffs = enemy.Buffs.FindAll(buff => buff.Moves > 0);

			enemy.Move(Enemies.ToArray(), player);
		}

		foreach (var buff in player.Buffs)
		{
			if (buff.Moves < 1)
			{
				buff.OnFinish(player);
			}
		}

		player.Buffs = player.Buffs.FindAll(buff => buff.Moves > 0);
	}
}