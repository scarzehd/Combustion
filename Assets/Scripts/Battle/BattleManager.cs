using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Combustion.Projectile;

namespace Combustion.Battle
{
	using UI;

	using Player;

    public class BattleManager : MonoBehaviour
    {
		public static BattleManager Instance { get; private set; }

		public PatternBase currentPattern;

		public AudioClip buttonSelectAudio;

		public Enemy[] enemies;

		// Start is called before the first frame update
		protected virtual void Start() {
			Instance = this;
		}

        // Update is called once per frame
        protected virtual void Update() {
			if (turnState == TurnState.Enemy && currentPattern != null) {
				currentPattern.Update();
			}
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
				StartEnemyTurn();
			}
			else
			{
				turnState = TurnState.Player;
				StartPlayerTurn();
			}
		}

		protected virtual void StartPlayerTurn() {
			currentPattern.Despawn();
			ArenaController.Instance.MoveAndScaleArena(ArenaController.Instance.textArenaPosition, ArenaController.Instance.textArenaSize, 1f);

			MenuController.Instance.ButtonBar.SetEnabled(true);

			PlayerController.Instance.gameObject.SetActive(false);
		}

		protected virtual void StartEnemyTurn() {
			if (currentPattern != null)
				currentPattern.Despawn();

			PlayerController.Instance.gameObject.SetActive(true);

			SpawnRandomPattern();

			MenuController.Instance.ButtonBar.SetEnabled(false);
		}

		public void SpawnRandomPattern() {
			if (currentPattern != null)
				currentPattern.Despawn();
			PatternBase[] patterns = Resources.LoadAll<PatternBase>("Patterns");
			currentPattern = patterns[Random.Range(0, patterns.Length)];
			currentPattern.Spawn();
		}
	}
}
