using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;

namespace Combustion.Battle.Nodes
{
	public class TestNode : ModifyNode {
		public Condition condition;
			
		public override Pattern Evaluate() {
			if (condition.Evaluate())
			{
				return child.Evaluate();
			}
			
			return null;
		}

		public class Condition {
			public bool Evaluate() {
				return false;
			}
		}
	}
}
