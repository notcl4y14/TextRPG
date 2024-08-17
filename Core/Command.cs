namespace Core;

abstract class Command
{
	public string Name;
	public string[] Alias;

	public Command Load()
	{
		return this;
	}

	public bool NameMatches(string match)
	{
		if (Name == match)
		{
			return true;
		}

		return Alias.Contains(match);
	}

	public abstract void Run(string[] args, ref Entity entity);
}