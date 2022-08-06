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

		private void Awake() {
			rb = gameObject.GetComponent<Rigidbody2D>();
		}

		private void Update() {
			lifetime -= Time.deltaTime;
			if (lifetime <= 0)
			{
				pattern.Projectiles.Remove(this);
				Destroy(gameObject);
			}
		}

		public static Projectile Spawn(Pattern pattern, Sprite sprite, float lifetime, Vector2 position = default(Vector2), params object[] list) {
			return null;
		}

		protected static GameObject CreateProjectileBase(Sprite sprite, Vector2 position = default(Vector2)) {
			GameObject go = new GameObject();
			go.transform.position = position;
			go.AddComponent<Rigidbody2D>().gravityScale = 0;
			SpriteRenderer ren = go.AddComponent<SpriteRenderer>();
			ren.sprite = sprite;
			ren.sortingOrder = 1;

			return go;
		}

		public virtual void Move(params object[] list) { }
		public virtual void OnHit(Collider2D collider) { }
	}
}