using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Projectile;

public class TestProjectile2 : Projectile
{
	private Vector2 direction;
	
	private float speed;

	private float acceleration;

	private bool moving;

	public new static TestProjectile2 Spawn(Pattern pattern, Sprite sprite, float lifetime, Vector2 position = default(Vector2), params object[] list) {
		GameObject go = CreateProjectileBase(sprite, position);
		TestProjectile2 proj = go.AddComponent<TestProjectile2>();
		proj.lifetime = lifetime;
		proj.pattern = pattern;

		if (list.Length > 0)
		{
			proj.direction = (Vector2)list[0];
		}
		else
		{
			proj.direction = Vector2.right;
		}

		if (list.Length > 1)
		{
			proj.speed = (float)list[1];
		}
		else
		{
			proj.speed = 1;
		}

		if (list.Length > 2)
		{
			proj.acceleration = (float)list[2];
		}
		else
		{
			proj.acceleration = 0;
		}

		return proj;
	}

	public override void Move(params object[] list) {
		if (list.Length > 0)
		{
			switch (list[0])
			{
				case Vector2 v:
					if (!moving)
					{
						transform.position = v;
					}
					break;
				case bool b:
					moving = b;
					break;
			}
		}

		if (list.Length > 1)
		{
			switch (list[0])
			{
				case Vector2 v:
					if (!moving)
					{
						transform.position = v;
					}
					break;
				case bool b:
					moving = b;
					break;
			}
		}

		if (moving)
		{
			speed += acceleration * Time.deltaTime;
			rb.velocity = direction * speed;
		}
	}
}
