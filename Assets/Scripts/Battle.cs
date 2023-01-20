using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    [System.Serializable]
    public abstract class Battle : MonoBehaviour {
        public Enemy[] enemies;

        public virtual void StartBattle() { }

        public abstract IAttack[] PickAttacks();

        public virtual void AttackEnding(IAttack[] attacks) { }
    }
}
