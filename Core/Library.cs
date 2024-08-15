namespace Core;

class Library<TKey, TValue>
{
	public static Dictionary<TKey, TValue> Registers = [];

	public static bool Register(TKey id, TValue value)
	{
		if (Registers.ContainsKey(id))
		{
			return false;
		}

		Registers[id] = value;
		return true;
	}

	public static TValue? GetFromID(TKey id)
	{
		if (!Registers.ContainsKey(id))
		{
			return default;
		}

		return Registers[id];
	}
}