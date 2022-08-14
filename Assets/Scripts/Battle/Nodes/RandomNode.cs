using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;

namespace Combustion.Battle.Nodes
{
	public class RandomNode : CompositeNode
	{
		public List<float> weights = new List<float>();

		public override Pattern Evaluate() {
			float totalWeight = 0;

			foreach (float weight in weights)
			{
				totalWeight += weight;
			}

			float random = Random.Range(0, totalWeight);

			for (int i = 0; i < children.Count; i++)
			{
				random -= weights[i];
				if (random <= 0)
				{
					return children[i].Evaluate();
				}
			}

			return null;
		}
	}
}