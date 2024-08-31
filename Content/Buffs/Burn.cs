using Common;
using Core;

namespace Content.Buffs;

class Burn : Buff
{
	public int Damage;

	public Burn(int damage)
	{
		Id = BuffID.Burn;
		Damage = damage;
		Icon = "[rgb(255,157,0)]B[/]";
	}

	public override void Step(Entity user)
	{
		TrpgConsole.MarkupLine($"{Icon}: {user.Id} takes {Damage} damage from [rgb(255,157,0)]burning[/]!");
		user.Hurt((uint)Damage);
		base.Step(user);
	}
}