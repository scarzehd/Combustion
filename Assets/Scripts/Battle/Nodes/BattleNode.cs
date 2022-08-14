using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;

namespace Combustion.Battle.Nodes
{
	public abstract  class BattleNode : ScriptableObject
	{
		public abstract Pattern Evaluate();

		[HideInInspector]
		public string guid;
		[HideInInspector]
		public Vector2 position;
	}
}