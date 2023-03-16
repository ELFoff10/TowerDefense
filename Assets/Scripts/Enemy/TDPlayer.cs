using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private Tower m_TowerPrefab;

        //[SerializeField] private Tower m_TowerArrowPrefab;
        //[SerializeField] private Tower m_TowerMagicPrefab;
        //[SerializeField] private Tower m_TowerBigMagicrefab;

        [SerializeField] private int m_Gold = 15;
        public int Gold => m_Gold;

        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }

        public static event Action<int> OnGoldUpdate;

        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_Gold);
        }

        public static void GoldUpdateUnSubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }

        public static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        public static void LifeUpdateUnSubscribe(Action<int> act)
        {
            OnLifeUpdate -= act;
        }

        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldUpdate(m_Gold);
        }

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);

            //if (towerAsset.name == "Tower1")
            //{
            //    Instantiate(m_TowerArrowPrefab, buildSite.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            //}
            //if (towerAsset.name == "Tower2")
            //{
            //    Instantiate(m_TowerMagicPrefab, buildSite.position + new Vector3(0, 0.38f, 0), Quaternion.identity);
            //}
            //if (towerAsset.name == "Tower3")
            //{
            //    Instantiate(m_TowerBigMagicrefab, buildSite.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            //}

            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);

            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;

            tower.GetComponentInChildren<Turret>().AssignLoadOut(towerAsset.TurretProperties);

            Destroy(buildSite.gameObject);
        }

        [SerializeField] private UpgradeAsset m_HealthUpgrade;
        [SerializeField] private UpgradeAsset m_MoneyUpgrade;

        private new void Awake()
        {
            base.Awake();
            var levelHealthUpgrade = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            var levelMoneyUpgrade = Upgrades.GetUpgradeLevel(m_MoneyUpgrade);

            TakeDamage(-levelHealthUpgrade * 3);
            m_Gold += levelMoneyUpgrade * 5;
        }

        //public void TakeDamage(int m_Damage)
        //{
        //    m_NumLives -= m_Damage;

        //    if (m_NumLives <= 0)
        //    {
        //        LevelSequenceController.Instance.FinishCurrentLevel(false);
        //    }
        //}
    }
}

