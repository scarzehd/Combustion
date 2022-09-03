using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Combustion.Projectile;

namespace Combustion.Battle
{
	using UI;

    public class BattleManager : MonoBehaviour
    {
		public static BattleManager Instance { get; private set; }

		public Pattern currentPattern;

		[SerializeField]
		private bool debugMode;

		// Start is called before the first frame update
		protected virtual void Start() {
			Instance = this;
		}

        // Update is called once per frame
        protected virtual void Update() {
			if (turnState == TurnState.Enemy && currentPattern != null) {
				if (currentPattern.IsActive)
				{
					currentPattern.Update();
				}
			}

			MenuController.Instance.SetDebug(debugMode);
		}

		public TurnState turnState = TurnState.Player;

		public enum TurnState {
			Player,
			Enemy
		}

		public virtual void AdvanceTurnState() {
			if (turnState == TurnState.Player)
			{
				turnState = TurnState.Enemy;
				if (debugMode)
				{
					MenuController.Instance.turnStateLabel.text = "Enemy Turn";
				}
				StartEnemyTurn();
			}
			else
			{
				turnState = TurnState.Player;
				if (debugMode)
				{
					MenuController.Instance.turnStateLabel.text = "Player Turn";
				}
				StartPlayerTurn();
			}
		}

		protected virtual void StartPlayerTurn() {
			currentPattern.Despawn();
			ArenaController.Instance.MoveAndScaleArena(ArenaController.Instance.textArenaPosition, ArenaController.Instance.textArenaSize, 1f);

			MenuController.Instance.buttonBar.SetEnabled(true);
		}

		protected virtual void StartEnemyTurn() {
			if (currentPattern != null)
				currentPattern.Despawn();

			SpawnRandomPattern();

			MenuController.Instance.buttonBar.SetEnabled(false);
		}

		public void SpawnRandomPattern() {
			if (currentPattern != null)
				currentPattern.Despawn();
			Pattern[] patterns = Resources.LoadAll<Pattern>("Patterns");
			currentPattern = patterns[Random.Range(0, patterns.Length)];
			currentPattern.Spawn();
		}
	}
}
