using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Projectile
{
	public abstract class Pattern : ScriptableObject
	{
		public List<Projectile> Projectiles = new List<Projectile>();

		public virtual bool IsActive => Projectiles.Count > 0;

		public virtual void Spawn() { }
		
		public virtual void Update() { }

		public virtual void SetupArena() { }

		public virtual void Despawn() {
			foreach (Projectile proj in Projectiles)
			{
				Destroy(proj.gameObject);
			}

			Projectiles = new List<Projectile>();
		}
	}
}
