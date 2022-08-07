using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;
public class TestProjectile : Projectile
{
	private Vector2 direction;

	private float speed;

	public new static Projectile Spawn(Pattern pattern, Sprite sprite, float lifetime, Vector2 position = default(Vector2), params object[] list) {
		GameObject go = CreateProjectileBase(sprite, position);
		TestProjectile proj = go.AddComponent<TestProjectile>();
		proj.lifetime = lifetime;
		proj.pattern = pattern;

		if (list.Length > 0)
		{
			proj.direction = (Vector2)list[0];
		}
		else
		{
			proj.direction = Vector2.up;
		}

		if (list.Length > 1)
		{
			proj.speed = (float)list[1];
		}
		else
		{
			proj.speed = 1;
		}

		return proj;
	}

	public override void Move(params object[] list) {
		if (list.Length > 0)
		{
			switch (list[0])
			{
				case Vector2 v:
					direction = v;
					break;
				case float f:
					speed = f;
					break;
			}
		}

		if (list.Length > 1)
		{
			switch(list[1])
			{
				case Vector2 v:
					direction = v;
					break;
				case float f:
					speed = f;
					break;
			}
		}

		rb.velocity = direction * speed;
	}
}
