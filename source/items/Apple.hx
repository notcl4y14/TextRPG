package source.items;

class Apple extends Item {
	public function new () {
		super("Apple", "Heals +5HP; 1/10 chance to get +10HP bonus", true);
	}

	// //////////////

	override public function use (user: Entity, target: Any, quiet: Bool = false) {
		var target: Entity = cast (target, Entity);
		target.heal(5, false, quiet);

		var bonus = Math.floor(Math.random() * 11);

		if (bonus == 1) {
			target.heal(10, false, quiet);
		}
	}
}