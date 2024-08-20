using Common;
using Content.Commands;
using Core;

namespace Content.Entities;

class Player : Entity
{
	// public int Bronze;
	// public int Silver;
	// public int Gold;

	public Player()
	{
		Id = EntityID.Player;
		Health = 125;
		HealthMax = 125;
		InventoryCapacity = 25;
		Currency = new Currency(bronze: 0, silver: 0, gold: 0);
	}

	public override void OnDead()
	{
		Game.GameOver();
	}
}