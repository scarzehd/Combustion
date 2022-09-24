using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Projectile
{
    public abstract class PatternBase : ScriptableObject
    {
        public List<ProjectileBase> projectiles = new List<ProjectileBase>();

        public virtual void Update() { }

        public virtual void Despawn() { }

        public virtual void Spawn() { }

        protected virtual void SetupArena() { }
    }
}
