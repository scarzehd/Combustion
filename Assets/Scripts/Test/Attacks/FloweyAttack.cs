using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    public class FloweyAttack : MonoBehaviour
    {
		protected void Awake() {
			GameObject go = GameObject.FindGameObjectsWithTag("Player")[0];

			transform.position = go.transform.position;
		}
	}
}
