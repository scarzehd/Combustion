using UnityEngine;

namespace Combustion.Battle
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
        public static BattleManager instance;

        public BattleState state;
        [SerializeField] private Enemy[] enemies;
/*        private bool enemyActed;
        private List<GameObject> attacks;*/

        [SerializeField] private Rect dialogBoxSize;
        [SerializeField] private float dialogBoxTransitionTime;

        public float attackTime;

		#region Unity Methods

		private void Start() {
            if (instance == null)
                instance = this;

            state = BattleState.Start;
		}

		private void Update() {
			if (SoulController.instance.hp <= 0)
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
                    EnemyTurn();

/*                    if (enemies.Length <= 0)
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
                    }*/
                    break;
                case BattleState.EndTurn:
                    SoulController.instance.gameObject.SetActive(false);
                    state = BattleState.Start;
                    break;
                case BattleState.Lose:

                    break;
            }
		}

		#endregion

		#region Private Methods

        private void EndPlayerTurn() {
            state = BattleState.EnemyTurn;
        }

        private void EndEnemyTurn() {
/*            foreach (GameObject go in attacks)
            {
                Destroy(go);
            }

            attacks = new List<GameObject>();

            enemyActed = false;
            state = BattleState.EndTurn;*/
        }

        private void EnemyTurn() {

        }

		#endregion

		#region Public Methds

        public void Lose() {
            switch (state)
            {
                case BattleState.PlayerTurn:
                    EndPlayerTurn();
                    break;
                case BattleState.EnemyTurn:
                    EndEnemyTurn();
                    BulletBox.instance.gameObject.SetActive(false);
                    break;
            }

            state = BattleState.Lose;
        }

		#endregion
	}
}
