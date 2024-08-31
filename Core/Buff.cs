namespace Core;

enum BuffID
{
	Null,
	Defense
}

class Buff
{
	public BuffID Id;
	public int Moves;
	public string Icon;

	public virtual void Step(Entity user)
	{
		Moves -= 1;
	}

	public virtual void OnFinish(Entity user)
	{
		return;
	}
}