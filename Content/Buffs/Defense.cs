using Core;

namespace Content.Buffs;

class Defense : Buff
{
	private int OldDefense;
	public int DefenseValue;

	public Defense(int defense)
	{
		Id = BuffID.Defense;
		DefenseValue = defense;
	}

	public override void Step(Entity user)
	{
		if (OldDefense == null)
		{
			OldDefense = user.AddDefense;
		}
		
		user.AddDefense += DefenseValue;
		base.Step(user);
	}

	public override void OnFinish(Entity user)
	{
		user.AddDefense = OldDefense;
	}
}