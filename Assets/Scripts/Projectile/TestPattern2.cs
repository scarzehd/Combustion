using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;
using Combustion.Player;
using Combustion.Battle;

[CreateAssetMenu(menuName = "Combustion/Test Pattern 2")]
public class TestPattern2 : Pattern
{
	[SerializeField]
	private int numProjectiles;

	[SerializeField]
	private float distanceLeft;

	[SerializeField]
	private Sprite projectileSprite;

	[SerializeField]
	private float projectileSpeed;

	[SerializeField]
	private float projectileAcceleration;

	[SerializeField]
	private float delayTime;
	private float delayCounter;

	private int currentProjectile;

	public override void Spawn() {
		delayCounter = delayTime;

		currentProjectile = 0;

		foreach (Projectile proj in Projectiles)
		{
			Destroy(proj.gameObject);
		}

		Projectiles = new List<Projectile>();

		for (int i = 0; i < numProjectiles; i++)
		{
			Vector2 pos = CalculateTrackPosition();

			Projectile proj = TestProjectile2.Spawn(this, projectileSprite, 3f + (delayTime * numProjectiles), pos, Vector2.right, projectileSpeed, projectileAcceleration);
			Projectiles.Add(proj);
		}
	}

	private Vector2 CalculateTrackPosition() {
		return new Vector2(ArenaController.Instance.transform.position.x, PlayerController.Instance.transform.position.y) + (Vector2.left * distanceLeft);
	}

	public override void Update() {
		foreach (Projectile proj in Projectiles)
		{
			proj.Move(CalculateTrackPosition());
		}
		
		if (delayCounter <= 0 && currentProjectile < Projectiles.Count)
		{
			Projectiles[currentProjectile].Move(true);
			currentProjectile++;
			delayCounter = delayTime;
		}

		delayCounter -= Time.deltaTime;
	}
}