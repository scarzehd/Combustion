using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Utility
{
    public class Scheduler : MonoBehaviour
    {
        public static Scheduler Instance { get; set; }

		private void Start() {
			Instance = this;
		}

		public static void Schedule(float delay, Action action) {
			Instance.StartCoroutine(Instance.InvokeAction(delay, action));
		}

		private IEnumerator InvokeAction(float delay, Action action) {

			if (delay == 0)
			{
				yield return null;
			} else
			{
				yield return new WaitForSeconds(delay);
			}

			action.Invoke();
		}
	}
}
