using Combustion.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle.Nodes
{
	public class SequenceNode : CompositeNode
	{
		public override Pattern Evaluate() {
			foreach(BattleNode child in children)
			{
				Pattern result = child.Evaluate();
				if (result != null)
				{
					return result;
				}
			}

			return null;
		}
	}
}