namespace Core;

class CommandInput
{
	public string Name;
	public List<string> Arguments;

	public CommandInput(string name, List<string> args)
	{
		Name = name;
		Arguments = args;
	}

	public static CommandInput FromString(string str)
	{
		string[] split = str.Split(" ");
		List<string> args = [];

		for (int i = 1; i < split.Length; i++)
		{
			args.Add(split[i]);
		}

		return new CommandInput(split[0], args);
	}
}