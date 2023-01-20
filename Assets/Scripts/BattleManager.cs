using UnityEngine;

namespace Combustion
{
    public enum BattleState {
        Start,
        PreTurn,
        PlayerTurn,
        EnemyTurn,
        EndTurn,
        Win,
        Lose
    }

    public abstract class BattleManager : MonoBehaviour
    {
		public Enemy[] enemies;

        public IAttack[] currentAttacks;

		public static BattleManager instance;

        public BattleState state;

        public Battle battle;

        [SerializeField] private Rect dialogBoxSize;
        [SerializeField] private float dialogBoxTransitionTime;

        [HideInInspector] public float attackTime;

		#region Unity Methods

		private void Start() {
            if (instance == null)
                instance = this;

            state = BattleState.PreTurn;

            Init();
		}

		private void Update() {
			if (SoulController.instance.hp <= 0)
				state = BattleState.Lose;

			switch (state)
            {
                case BattleState.PreTurn:
					PreRound();
					state = BattleState.PlayerTurn;
					break;
                case BattleState.PlayerTurn:
					PlayerTurn();
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
                    state = BattleState.PreTurn;
                    break;
                case BattleState.Lose:
                    break;
            }
		}

		#endregion

		#region Private Methods

        private void EndPlayerTurn() {
            
        }

        private void EndEnemyTurn() {
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

		#region Protected Methods

        protected virtual void Init() { }

        protected virtual void PreRound() { }

        protected virtual void PlayerTurn() { }

		protected virtual void EnemyTurn() { }

		#endregion
	}
}
