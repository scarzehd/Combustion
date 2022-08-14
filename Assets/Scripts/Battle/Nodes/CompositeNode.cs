using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle.Nodes
{
	public abstract class CompositeNode : BattleNode
	{
		public List<BattleNode> children = new List<BattleNode>();
	}
}
