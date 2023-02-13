using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Color Color = Color.white;
        public Vector2 SpriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController Animations;

        [Header("Игровые параметры")]
        public float MoveSpeed = 1f;
        public int HP = 1;
        public int Score = 1;
        public float Radius = 0.19f;
        public int Damage = 1;
        public int Gold = 1;
    }
}