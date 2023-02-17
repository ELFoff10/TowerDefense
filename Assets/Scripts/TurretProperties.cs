using UnityEngine;

namespace TowerDefense
{
    public enum TurretMode
    {
        Primary,
        Secondary,
        Auto            
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// Ссылка на префаб прожектайла который будет стрелять турель.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// Скорострельность турели. Меньше - лучше.
        /// </summary>
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// Сколько энергии кушает за выстрел.
        /// </summary>
        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;

        /// <summary>
        /// Сколько энергии кушает за выстрел.
        /// </summary>
        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;

        /// <summary>
        /// Звук выстрела. Это на ДЗ добавить самим звук при выстреле.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}