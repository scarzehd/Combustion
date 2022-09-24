using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;

public class DirectProjectile : ProjectileBase
{
	public DirectProjectile(Vector2 position, Vector2 velocity) {
		gameObject = new GameObject();
		rb = gameObject.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		rb.velocity = velocity;
		ren = gameObject.AddComponent<SpriteRenderer>();
		ren.sortingOrder = 1;
		gameObject.transform.position = position;
	}

	public override void Update() { }

	public void SetVelocity(Vector2 velocity) {
		rb.velocity = velocity;
	}
}

