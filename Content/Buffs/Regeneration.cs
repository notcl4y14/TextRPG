using Common;
using Core;

namespace Content.Buffs;

class Regeneration : Buff
{
	public int Health;

	public Regeneration(int health)
	{
		Id = BuffID.Regeneration;
		Health = health;
		Icon = "[rgb(167,242,61)]R[/]";
	}

	public override void Step(Entity user)
	{
		TrpgConsole.MarkupLine($"{Icon}: {user.Id} recovers {Health} HP from [rgb(167,242,61)]regenerating[/]!");
		user.Heal((uint)Health);
		base.Step(user);
	}
}