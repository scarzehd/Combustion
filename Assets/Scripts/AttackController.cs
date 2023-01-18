using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Battle
{
    public abstract class AttackController
    {
        public float time;

        public List<EnemyProjectile> projectiles;

        public AttackController() {
            time = 0;
            projectiles = new List<EnemyProjectile>();
        }

        public virtual void UpdateAttack() {
            time += Time.deltaTime;
        }
    }
}
