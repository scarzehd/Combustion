using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;
namespace Combustion.Battle
{
    public class BattleManager : MonoBehaviour
    {
		public static BattleManager Instance { get; private set; }

		public Pattern currentPattern;

		// Start is called before the first frame update
		void Start() {
			Instance = this;
		}

        // Update is called once per frame
        void Update() {
			if (currentPattern != null)
			{
				if (currentPattern.Projectiles.Count > 0)
				{
					currentPattern.Update();
				}
			}
        }

		public TurnState turnState = TurnState.Enemy;

		public enum TurnState {
			Player,
			Enemy
		}
	}
}
