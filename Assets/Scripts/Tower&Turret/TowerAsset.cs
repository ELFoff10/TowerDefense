using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        [SerializeField] private UpgradeAsset m_RequiredUpgrade;

        [SerializeField] private int m_RequiredUpgradeLevel;

        public int GoldCost;

        public TurretProperties TurretProperties;

        public Sprite GUISprite, Sprite;

        public bool IsAvailable() => !m_RequiredUpgrade ||
            m_RequiredUpgradeLevel <= Upgrades.GetUpgradeLevel(m_RequiredUpgrade);

        public TowerAsset[] UpgradesTo;
    }
}

