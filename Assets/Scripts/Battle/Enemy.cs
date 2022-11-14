using UnityEngine;

namespace Combustion.Battle
{
    using Projectile;
	using UnityEngine.Events;

	public abstract class Enemy : MonoBehaviour
    {
        public SerializableDictionary<string, UnityEvent> actChoices;

        public float defense;

        public float hp;

        public PatternBase[] patterns;

        public abstract PatternBase ChoosePattern();
    }
}
