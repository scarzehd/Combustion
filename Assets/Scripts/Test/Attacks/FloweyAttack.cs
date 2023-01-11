using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    public class FloweyAttack : EnemyAttack
    {
		protected override void Awake() {
			base.Awake();

			GameObject go = GameObject.FindGameObjectsWithTag("Player")[0];

			transform.position = go.transform.position;
		}
	}
}
