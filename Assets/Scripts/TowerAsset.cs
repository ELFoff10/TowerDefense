using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        [SerializeField] private int m_GoldCost;
        public int GoldCost => m_GoldCost;

        [SerializeField] private Sprite m_GUISprite;
        public Sprite GUISprite => m_GUISprite;

        [SerializeField] private Sprite m_Sprite;
        public Sprite Sprite => m_Sprite;

        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        [SerializeField] private float m_RateOfFire = 0.8f;
        public float RateOfFire => m_RateOfFire;
    }
}

