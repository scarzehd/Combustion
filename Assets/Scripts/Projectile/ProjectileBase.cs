using System;
using UnityEngine;

namespace Combustion.Projectile
{
    public abstract class ProjectileBase
    {
        public GameObject gameObject;

        public Action<ProjectileBase> onHit;

        public Rigidbody2D rb;

        public SpriteRenderer ren;

        public virtual void Move() { }

        public virtual void Update() { }

        public void SetSprite(Sprite sprite) {
            ren.sprite = sprite;
        }

        public virtual void Destroy() {
			GameObject.Destroy(gameObject);
		}
    }
}
