using Common;
using Content.Commands;
using Core;

namespace Content.Entities;

class Player : Entity
{
	public Player()
	{
		Id = EntityID.Player;
		Health = 125;
		HealthMax = 125;
		Inventory = new Common.Inventory(25);
		Cash = new Currency(bronze: 0, silver: 0, gold: 0);
	}

	public override void OnDead()
	{
		Game.GameOver();
	}
}