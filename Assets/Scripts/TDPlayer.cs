using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
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

        [SerializeField] private int m_Gold = 15;

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

        [SerializeField] private Tower m_TowerPrefab;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);
            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.Sprite;
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

