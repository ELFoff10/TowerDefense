using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : MonoSingleton<Upgrades>
    {
        [SerializeField] private UpgradeSave[] m_Save;

        public const string filename = "upgrades.dat";

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset UpgradeAsset;
            public int Level = 1; // у ментора 0
        }

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref m_Save);
        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.UpgradeAsset == asset)
                {
                    upgrade.Level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.m_Save);
                }
            }
        }

        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.m_Save)
            {
                for (int i = 0; i < upgrade.Level; i++)
                {
                    result += upgrade.UpgradeAsset.CostByLevel[i];
                }
            }
            return result;
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.UpgradeAsset == asset)
                {
                    return upgrade.Level;
                }
            }
            return 0;
        }
    }
}

