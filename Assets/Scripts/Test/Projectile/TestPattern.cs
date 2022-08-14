using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;
using Combustion.Player;

[CreateAssetMenu(menuName = "Combustion/TestPattern")]
public class TestPattern : Pattern
{
	[SerializeField]
	private int numProjectiles;

	[SerializeField]
	private float distanceFromPlayer;

	[SerializeField]
	private Sprite projectileSprite;

	[SerializeField]
	private float projectileSpeed;

	[SerializeField]
	private float delayTime;
	private float delayCounter;

	private int nextProjectile = 0;

	public override void Spawn() {
		Despawn();
		
		for (int i = 0; i < numProjectiles; i++)
		{
			Vector2 playerPos = PlayerController.Instance.transform.position;

			Vector2 pos = new Vector2(
				playerPos.x + distanceFromPlayer * Mathf.Cos(i * 2 * Mathf.PI / numProjectiles),
				playerPos.y + distanceFromPlayer * Mathf.Sin(i * 2 * Mathf.PI / numProjectiles)
			);

			Vector2 direction = -(pos - playerPos).normalized;

			Projectile proj = TestProjectile.Spawn(this, projectileSprite, 2f + (numProjectiles * delayTime), pos, direction, projectileSpeed);
			Projectiles.Add(proj);
		}
	}

	public override void Despawn() {
		delayCounter = delayTime;

		nextProjectile = 1;
		base.Despawn();
	}

	public override void Update() {
		for (int i = 0; i < nextProjectile; i++)
		{
			Projectiles[i].Move();
		}

		if (delayCounter <= 0 && nextProjectile < Projectiles.Count)
		{
			nextProjectile++;
			delayCounter = delayTime;
		}

		delayCounter -= Time.deltaTime;
	}
}