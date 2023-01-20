using System.Collections.Generic;
using UnityEngine;

namespace Combustion {

	public class EnemyProjectile : MonoBehaviour
	{
		public int damage;

		public float time;


		private Dictionary<string, object> vars = new Dictionary<string, object>();

		private void Awake() {
			time = 0f;
		}

		private void Update() {
			time += Time.deltaTime;
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			collision.TryGetComponent(out SoulController player);

			if (player != null)
			{
				player.TakeDamage(damage);
			}
		}

		public T GetValue<T>(string key) {
			if (vars[key] is T result)
				return result;
			else return default(T);
		}

		public void SetValue(string key, object value) {
			vars[key] = value;
		}
	}
}
