using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
	//Deprecated
    public class EnemyAttack : MonoBehaviour
    {
        public bool TurnFinished { get; private set; }

        [SerializeField] private int numPatterns;

		protected void Awake() {
			TurnFinished = false;

			int patternIndex = Random.Range(0, numPatterns - 1);
			GetComponent<Animator>().SetInteger("Pattern", patternIndex);
		}

		public void AttackDone() {
			TurnFinished = true;
		}
	}
}
