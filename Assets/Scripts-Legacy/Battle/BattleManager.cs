using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Combustion.Projectile;

namespace Combustion.Battle
{
	using UI;

	using Player;
	using Util;

	public class BattleManager : MonoBehaviour
    {
		public static BattleManager Instance { get; private set; }

		public Pattern currentPattern;

		public Enemy currentEnemy;

		// Start is called before the first frame update
		protected virtual void Start() {
			Instance = this;
		}

        // Update is called once per frame
        protected virtual void Update() {
			if (turnState == TurnState.Enemy && currentPattern != null) {
				currentPattern.Update();
			}

			if (turnState == TurnState.Start)
			{
				turnState = TurnState.Player;

				StartPlayerTurn();
			}
		}

		public TurnState turnState;

		public enum TurnState {
			Player,
			Enemy,
			Start
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
			currentPattern?.Despawn();
			ArenaController.Instance.MoveAndScaleArena(ArenaController.Instance.textArenaPosition, ArenaController.Instance.textArenaSize, 1f);

			MenuManager.Instance.ButtonBar.SetEnabled(true);

			MenuManager.Instance.SelectCurrentButton();

			PlayerController.Instance.gameObject.SetActive(false);
		}

		protected virtual void StartEnemyTurn() {
			currentPattern?.Despawn();

			PlayerController.Instance.gameObject.SetActive(true);

			currentPattern = currentEnemy.ChoosePattern();

			currentPattern.Spawn();

			MenuManager.Instance.ButtonBar.SetEnabled(false);

			MenuManager.Instance.ClearTextBox();
		}
	}
}
