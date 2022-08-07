using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Combustion.Projectile;
namespace Combustion.Battle
{
    public class BattleManager : MonoBehaviour
    {
		public static BattleManager Instance { get; private set; }

		public Pattern currentPattern;

		[SerializeField]
		private TextMeshProUGUI turnStateText;

		// Start is called before the first frame update
		void Start() {
			Instance = this;
		}

        // Update is called once per frame
        void Update() {
			if (turnState == TurnState.Enemy && currentPattern != null) {
				currentPattern.Update();
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

		private void StartPlayerTurn() { }

		private void StartEnemyTurn() {
			SpawnRandomPattern();
		}

		public void SpawnRandomPattern() {
			if (currentPattern != null)
				currentPattern.Reset();
			Pattern[] patterns = Resources.LoadAll<Pattern>("Patterns");
			currentPattern = patterns[Random.Range(0, patterns.Length)];
			currentPattern.Spawn();
		}
	}
}
