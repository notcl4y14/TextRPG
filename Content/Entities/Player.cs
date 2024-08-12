using Core;

namespace Content.Entities;

class Player : Entity
{
	public Player()
	{
		Id = EntityID.Player;
		Health = 125;
		HealthMax = 125;
	}
}