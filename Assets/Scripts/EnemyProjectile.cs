using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    public class EnemyProjectile : MonoBehaviour
    {
        public int Damage { get; private set; }

		private void OnTriggerEnter2D(Collider2D collision) {
			collision.TryGetComponent(out SoulController player);

			if (player != null)
			{
				player.TakeDamage(Damage);
			}
		}
	}
}