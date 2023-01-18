using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Projectile
{
    public abstract class Pattern : ScriptableObject
    {
        public List<Projectile> projectiles = new List<Projectile>();


        public virtual void Update() { }

        public virtual void Despawn() { }

        public virtual void Spawn() { }

        protected virtual void SetupArena() { }

        protected static Projectile CreateProjectile(Vector2 position, Sprite sprite, Vector2 velocity = default) {
            GameObject go = new GameObject();
            go.transform.position = position;
            go.AddComponent<SpriteRenderer>().sprite = sprite;
            Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
            rb.velocity = velocity;
            rb.gravityScale = 0f;
            return go.AddComponent<Projectile>();
        }
    }
}
