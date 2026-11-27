using System;
using System.Collections.Generic;
using System.Text;

namespace StardewValley {
	class Tree {
		private IFruitProductionStrategy strategy;
		
		public Tree(IFruitProductionStrategy strategy) {
			this.strategy = strategy;
		}

		public List<IFruit> Harvest() {
			return new List<IFruit> {
				strategy.Produce(),
				strategy.Produce(),
				strategy.Produce()
			};
		}
	}
}

