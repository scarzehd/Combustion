using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Projectile
{
    public class Projectile : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

		private void Awake() {
			SpriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody = GetComponent<Rigidbody2D>();

            SpriteRenderer.sortingOrder = 1;
		}

		public void SetVelocity(Vector2 velocity) {
            Rigidbody.velocity = velocity;
        }
    }
}
