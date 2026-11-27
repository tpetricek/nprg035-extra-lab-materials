using StardewValley;

Tree[] trees = new Tree[] {
	new Tree(new SwitchingProductionStrategy() {
		DefaultFactory = new AppleFactory {  MakesGoldenApples = false },
		SpecialFactory = new AppleFactory {  MakesGoldenApples = true },
		Rate = 3
	}),
	new Tree(new SimpleProductionStrategy() { Factory = new PeachFactory() }),
	new Tree(new SwitchingProductionStrategy() {
		DefaultFactory = new FrnakovnikFactory {  DefaultEnergy = 2 },
		SpecialFactory = new FrnakovnikFactory {  DefaultEnergy = 4 },
		Rate = 2
	})
};

foreach(var tree in trees) {
	foreach(var a in tree.Harvest()) { 
		if (a is IEdible edible) edible.Eat();
	}
}