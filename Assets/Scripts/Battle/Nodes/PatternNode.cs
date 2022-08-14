using Combustion.Projectile;
using UnityEditor;

namespace Combustion.Battle.Nodes
{
	public class PatternNode : ActionNode
	{
		public Pattern pattern;

		public override Pattern Evaluate() {
			return pattern;
		}

		private void OnValidate() {
			if (pattern != null)
			{
				name = pattern.name;
			}
		}
	}
}
