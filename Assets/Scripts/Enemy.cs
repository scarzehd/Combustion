using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Combustion/New Enemy")]
    public class Enemy : ScriptableObject
    {
        public int hp;
        public int charmHp;

        public Sprite sprite;

        public GameObject[] attacks;
    }
}
