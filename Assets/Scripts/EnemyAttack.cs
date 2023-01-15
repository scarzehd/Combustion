using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    public class EnemyAttack : MonoBehaviour
    {
        public bool TurnFinished { get; private set; }

        [SerializeField] private int numPatterns;

		//this is virtual so you can change the spawning conditions of the attack
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
