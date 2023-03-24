using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private Tower m_TowerPrefab;

        [SerializeField] private int m_Gold = 15;

        [SerializeField] private int m_AbilityEnergy = 30;

        public int AbilityEnergy => m_AbilityEnergy;

        [SerializeField] private UpgradeAsset m_HealthUpgrade, m_MoneyUpgrade, m_FireAbilityUpgrade, m_SlowAbilityUpgrade;
        [SerializeField] private GameObject m_FireAbility;
        [SerializeField] private GameObject m_SlowAbility;

        protected override void Awake()
        {
            base.Awake();
            var healthBonus = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            var moneyBonus = Upgrades.GetUpgradeLevel(m_MoneyUpgrade);
            var fireAbility = Upgrades.GetUpgradeLevel(m_FireAbilityUpgrade);
            var slowAbility = Upgrades.GetUpgradeLevel(m_SlowAbilityUpgrade);

            if (gameObject != null)
            {
                m_FireAbility.gameObject.SetActive(false);
                m_SlowAbility.gameObject.SetActive(false);
            }

            if (fireAbility > 0)
            {
                m_FireAbility.gameObject.SetActive(true);
                Abilities.Instance.UpgradeFireAbility(fireAbility);
            }

            if (slowAbility > 0)
            {
                m_SlowAbility.gameObject.SetActive(true);
                Abilities.Instance.UpgradeSlowAbility(slowAbility);
            }

            if (healthBonus > 0)
            {
                TakeDamage(-healthBonus * 5);
            }

            if (moneyBonus > 0)
            {
                m_Gold += moneyBonus * 10;
            }
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

        public event Action<int> OnEnergyUpdate;

        public void EnergyUpdateSubscribe(Action<int> act)
        {
            OnEnergyUpdate += act;
            act(m_AbilityEnergy);
        }

        public void EnergyUpdateUnSubscribe(Action<int> act)
        {
            OnEnergyUpdate -= act;
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

        public void ChangeEnergy(int change)
        {
            m_AbilityEnergy += change;
            OnEnergyUpdate(m_AbilityEnergy);
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);

            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);

            tower.SetTurretProperties(towerAsset);

            //tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;

            tower.GetComponentInChildren<Turret>().AssignLoadOut(towerAsset.TurretProperties);

            Destroy(buildSite.gameObject);
        }
    }
}

