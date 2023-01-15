using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    public enum BattleState {
        Start,
        PlayerTurn,
        EnemyTurn,
        EndTurn,
        Win,
        Lose
    }

    public class BattleManager : MonoBehaviour
    {
        public BattleState state;
        [SerializeField] private Enemy[] enemies;
        private bool enemyActed;
        [SerializeField] private List<GameObject> attacks;

        [SerializeField] private SoulController player;

		#region Unity Methods

		private void Start() {
            state = BattleState.Start;
            enemyActed = false;
		}

		private void Update() {
			if (player.hp <= 0)
				state = BattleState.Lose;

			switch (state)
            {
                case BattleState.Start:
                    state = BattleState.PlayerTurn;
                    break;
                case BattleState.PlayerTurn:
                    EndPlayerTurn();
                    break;
                case BattleState.EnemyTurn:
                    if (enemies.Length <= 0)
                        EndEnemyTurn();
                    else if (!enemyActed)
                    {
                        player.gameObject.SetActive(true);

						enemyActed = true;

						foreach (Enemy enemy in enemies)
                        {
                            int attackIndex = Random.Range(0, enemy.attacks.Length - 1);

							attacks.Add(Instantiate(enemy.attacks[attackIndex], Vector3.zero, Quaternion.identity));
                        }
                    } else
                    {
                        bool enemyFinished = true;

                        foreach (GameObject attack in attacks)
                        {
                            if (!attack.GetComponent<EnemyAttack>().TurnFinished)
                                enemyFinished = false;
                        }

                        if (enemyFinished)
                            EndEnemyTurn();
                    }
					break;
                case BattleState.EndTurn:
                    player.gameObject.SetActive(false);
                    state = BattleState.Start;
                    break;
            }
		}

		#endregion

		#region Private Methods

        private void EndPlayerTurn() {
            state = BattleState.EnemyTurn;
        }

        private void EndEnemyTurn() {
            foreach (GameObject go in attacks)
            {
                Destroy(go);
            }

            attacks = new List<GameObject>();

            enemyActed = false;
            state = BattleState.EndTurn;
        }

		#endregion
	}
}
