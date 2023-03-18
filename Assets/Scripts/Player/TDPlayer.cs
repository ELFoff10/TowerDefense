using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private Tower m_TowerPrefab;

        [SerializeField] private int m_Gold = 15;

        [SerializeField] private UpgradeAsset m_HealthUpgrade, m_MoneyUpgrade;

        private void Start()
        {
            var levelHealthUpgrade = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            var levelMoneyUpgrade = Upgrades.GetUpgradeLevel(m_MoneyUpgrade);

            TakeDamage(-levelHealthUpgrade * 3);
            m_Gold += levelMoneyUpgrade * 5;
        }

        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }

        public event Action<int> OnGoldUpdate;

        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_Gold);
        }

        public void GoldUpdateUnSubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }

        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        public void LifeUpdateUnSubscribe(Action<int> act)
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

            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);

            tower.SetTurretProperties(towerAsset);

            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;

            tower.GetComponentInChildren<Turret>().AssignLoadOut(towerAsset.TurretProperties);

            Destroy(buildSite.gameObject);
        }
    }
}

