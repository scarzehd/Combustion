using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Projectile
{
	public abstract class Projectile : MonoBehaviour
	{
		protected Rigidbody2D rb;

		protected Pattern pattern;

		protected float lifetime;

		protected virtual void Awake() {
			rb = gameObject.GetComponent<Rigidbody2D>();
		}

		protected virtual void Update() {
			lifetime -= Time.deltaTime;
			if (lifetime <= 0)
			{
				pattern.Projectiles.Remove(this);
				Destroy(gameObject);
			}
		}

		protected static T CreateProjectile<T>(Sprite sprite, Vector2 position = default(Vector2)) where T : Projectile {
			GameObject go = new GameObject();
			go.transform.position = position;
			go.AddComponent<Rigidbody2D>().gravityScale = 0;
			SpriteRenderer ren = go.AddComponent<SpriteRenderer>();
			ren.sprite = sprite;
			ren.sortingOrder = 1;

			return go.AddComponent<T>();
		}

		public virtual void OnHit(Collider2D collider) { }
	}
}