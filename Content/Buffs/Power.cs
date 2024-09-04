using Core;

namespace Content.Buffs;

class Power : Buff
{
	private int OldDamage;
	public int DamageValue;

	public Power(int damage)
	{
		Id = BuffID.Power;
		DamageValue = damage;
		Icon = "[rgb(181,201,232)]P[/]";
	}

	public override void Step(Entity user)
	{
		if (OldDamage == null)
		{
			OldDamage = user.AddDamage;
		}
		
		user.AddDamage = DamageValue;
		base.Step(user);
	}

	public override void OnFinish(Entity user)
	{
		user.AddDefense = OldDamage;
	}
}