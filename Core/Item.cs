namespace Core;

enum ItemID
{
	Null,
	Apple
}

abstract class Item
{
	public ItemID Id;
	public string Description;
	public int Amount;

	public string AmountString
	{
		get => Amount != 1 ? $"x{Amount}" : "";
	}

	public abstract void Use(Entity user);
}