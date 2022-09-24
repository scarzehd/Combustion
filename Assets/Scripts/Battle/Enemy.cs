using UnityEngine;

namespace Combustion.Battle
{
    using Projectile;
	using UnityEngine.Events;

	public class Enemy : MonoBehaviour
    {
        public SerializableDictionary<string, UnityEvent> actChoices;

        public PatternBase[] patterns;
    }
}
