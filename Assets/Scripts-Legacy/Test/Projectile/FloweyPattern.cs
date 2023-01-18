using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;
using Combustion.Player;
using Combustion.Battle;

[CreateAssetMenu(menuName = "Combustion/Test/Flowey Pattern", fileName = "Flowey Pattern")]
public class FloweyPattern : Pattern
{
	public int numProjectiles;

	public float projectileDelay;
	private float delayCounter;

	private int currentProjectileIndex;

	public float projectileSpeed;

	public float radius;

	public float projectileLifetime;
	private float lifetimeCounter;

	public Sprite[] projectileSprites;

	private int projectileSpriteIndex;

	public float projectileAnimationTime;
	private float projectileAnimationCounter;

	private Vector2 center;

	private bool projectilesMoving;

	public AudioClip projectileSpawnClip;

	public override void Spawn() {
		Despawn();

		center = PlayerController.Instance.transform.position;

		for (int i = 0; i < numProjectiles; i++)
		{
			Vector2 pos = new Vector2(
				center.x + radius * Mathf.Cos(i * 2 * Mathf.PI / numProjectiles),
				center.y + radius * Mathf.Sin(i * 2 * Mathf.PI / numProjectiles)
			);

			Projectile proj = CreateProjectile(pos, projectileSprites[projectileSpriteIndex]);

			projectiles.Add(proj);

			proj.gameObject.SetActive(false);
		}

		lifetimeCounter = projectileLifetime;

		projectileAnimationCounter = projectileAnimationTime;

		delayCounter = projectileDelay;

		SetupArena();
	}

	public override void Despawn() {
		if (projectiles != null)
		{
			foreach (Projectile proj in projectiles)
			{
				Destroy(proj.gameObject);
			}
		}

		projectiles = new List<Projectile>();

		currentProjectileIndex = 0;

		projectilesMoving = false;

		projectileSpriteIndex = 0;
	}

	public override void Update() {
		if (!ArenaController.Instance.IsMoving)
		{
			lifetimeCounter -= Time.deltaTime;

			if (lifetimeCounter <= 0)
			{
				Despawn();

				BattleManager.Instance.AdvanceTurnState();
			}

			delayCounter -= Time.deltaTime;

			if (currentProjectileIndex < projectiles.Count)
			{
				if (delayCounter <= 0)
				{
					projectiles[currentProjectileIndex].gameObject.SetActive(true);

					delayCounter = projectileDelay;

					currentProjectileIndex++;

					SoundManager.Instance.PlaySound(projectileSpawnClip);
				}
			}
			else
			{
				if (!projectilesMoving)
				{
					foreach (Projectile proj in projectiles)
					{
						Vector2 velocity = -(proj.transform.position).normalized * projectileSpeed;

						proj.SetVelocity(velocity);
					}

					projectilesMoving = true;
				}
			}
		}

		projectileAnimationCounter -= Time.deltaTime;

		if (projectileAnimationCounter <= 0)
		{
			projectileSpriteIndex++;

			if (projectileSpriteIndex >= projectileSprites.Length)
				projectileSpriteIndex = 0;

			projectileAnimationCounter = projectileAnimationTime;
		}

		foreach (Projectile proj in projectiles)
		{
			if (proj.gameObject.activeSelf)
			{
				proj.SpriteRenderer.sprite = projectileSprites[projectileSpriteIndex];
			}
		}
	}

	protected override void SetupArena() {
		ArenaController.Instance.MoveAndScaleArena(0, 0, 2, 2, 1);
	}
}
