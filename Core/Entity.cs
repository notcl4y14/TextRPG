namespace Core;

enum EntityID
{
	Null,
	Player
}

class Entity
{
	public EntityID Id;
	public int Health;
	public int HealthMax;

	public string HealthString
	{
		get => $"{Health}/{HealthMax}";
	}

	public int HealthPercent
	{
		get => Health / HealthMax * 100;
	}

	public void Heal(uint delta)
	{
		Health += (int)delta;

		if (Health > HealthMax)
		{
			Health = HealthMax;
		}
	}

	public void Hurt(uint delta)
	{
		Health -= (int)delta;

		if (Health < 0)
		{
			Health = 0;
		}
	}
}