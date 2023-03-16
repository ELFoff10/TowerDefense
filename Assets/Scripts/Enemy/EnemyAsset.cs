using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class EnemyAsset : ScriptableObject
    {
        [Header("Enemy Settings")]
        [Space(10)]
        public RuntimeAnimatorController Animations;
        public Vector2 SpriteScale = new Vector2(3, 3);
        public Color Color = Color.white;
        public float Speed = 1f;
        public int HP = 1;
        public int Armor = 0;
        public Enemy.ArmorType ArmorType;
        public int Score = 1;
        public float Radius = 0.19f;
        public int Damage = 1;
        public int Gold = 1;
    }
}