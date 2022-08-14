using Combustion.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle.Nodes
{
	public class RootNode : BattleNode
	{
		[HideInInspector]
		public BattleNode child;

		public override Pattern Evaluate() {
			return child.Evaluate();
		}

	}
}