using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : MonoSingleton<Upgrades>
    {
        public const string filename = "upgrades.dat";

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset m_Asset;
            public int m_Level = 1;
        }

        [SerializeField] private UpgradeSave[] m_Save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref m_Save);
        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.m_Asset == asset)
                {
                    upgrade.m_Level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.m_Save);
                }
            }
        }

        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.m_Save)
            {
                for (int i = 0; i < upgrade.m_Level; i++)
                {
                    result += upgrade.m_Asset.m_CostByLevel[i];
                }
            }
            return result;
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.m_Asset == asset)
                {
                    return upgrade.m_Level;
                }
            }
            return 0;
        }
    }
}

