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
		InventoryCapacity = 25;
	}

	public override void OnDead()
	{
		Game.GameOver();
	}
}