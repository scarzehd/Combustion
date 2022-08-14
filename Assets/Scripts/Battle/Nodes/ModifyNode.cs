using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle.Nodes
{
	public abstract class ModifyNode : BattleNode
	{
		[HideInInspector]
		public BattleNode child;
	}
}