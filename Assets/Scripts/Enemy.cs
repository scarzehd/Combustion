using UnityEngine;

namespace Combustion.Battle
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Combustion/New Enemy")]
    public class Enemy : ScriptableObject
    {
        public int hp;
        public int charmHp;

        public Sprite sprite;

        public IAttack[] attacks;
    }
}
