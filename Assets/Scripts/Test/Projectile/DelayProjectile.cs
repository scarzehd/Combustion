using System;
using UnityEngine;

using Combustion.Projectile;

public class DelayProjectile : ProjectileBase
{
	private bool triggered = false;

	private float delayTimer;

	private Action<DelayProjectile> onDelayEnd;

	public DelayProjectile(float delayTime, Vector2 position, Action<DelayProjectile> delayEnd = null) {
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
		gameObject.AddComponent<SpriteRenderer>().sortingOrder = 1;
		gameObject.transform.position = position;

		delayTimer = delayTime;

		onDelayEnd = delayEnd;
	}

	public override void Update() {
		delayTimer -= Time.deltaTime;

		if (delayTimer <= 0 && !triggered)
		{
			if (onDelayEnd != null)
			{
				onDelayEnd(this);
			}

			triggered = true;
		}
	}
}
