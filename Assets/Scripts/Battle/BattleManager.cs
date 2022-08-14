using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Combustion.Projectile;
using Combustion.Battle.Nodes;

namespace Combustion.Battle
{
    public class BattleManager : MonoBehaviour
    {
		public static BattleManager Instance { get; private set; }

		public Pattern currentPattern;

		[SerializeField]
		private TextMeshProUGUI turnStateText;

		public BattleTree tree;

		// Start is called before the first frame update
		void Start() {
			Instance = this;
		}

        // Update is called once per frame
        void Update() {
			if (turnState == TurnState.Enemy && currentPattern != null) {
				if (currentPattern.IsActive)
				{
					currentPattern.Update();
				} else
				{
					AdvanceTurnState();
				}
			}
        }

		public TurnState turnState = TurnState.Player;

		public enum TurnState {
			Player,
			Enemy
		}

		public void AdvanceTurnState() {
			if (turnState == TurnState.Player)
			{
				turnState = TurnState.Enemy;
				turnStateText.text = "Enemy Turn";
				StartEnemyTurn();
			}
			else
			{
				turnState = TurnState.Player;
				turnStateText.text = "Player Turn";
				StartPlayerTurn();
			}
		}

		private void StartPlayerTurn() {
			currentPattern.Despawn();
			ArenaController.Instance.MoveAndScaleArena(ArenaController.Instance.textArenaPosition, ArenaController.Instance.textArenaSize, 1f);
		}

		private void StartEnemyTurn() {
			if (currentPattern != null)
				currentPattern.Despawn();

			currentPattern = tree.ChoosePattern();
			currentPattern.Spawn();
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
