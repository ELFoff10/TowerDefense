using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_Gold = 15;
        [SerializeField] private Tower m_TowerArrowPrefab;
        [SerializeField] private Tower m_TowerMagicPrefab;
        [SerializeField] private Tower m_TowerBigMagicrefab;

        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }

        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_Gold);
        }
        private static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
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

            if (towerAsset.name == "Tower1")
            {
                Instantiate(m_TowerArrowPrefab, buildSite.position, Quaternion.identity);
            }
            if (towerAsset.name == "Tower2")
            {
                Instantiate(m_TowerMagicPrefab, buildSite.position, Quaternion.identity);
            }
            if (towerAsset.name == "Tower3")
            {
                Instantiate(m_TowerBigMagicrefab, buildSite.position, Quaternion.identity);
            }

            /* var tower = */
            //tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;

            //tower.GetComponentInChildren<Turret>().Turre = towerAsset.m_Projectile;

            Destroy(buildSite.gameObject);
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

