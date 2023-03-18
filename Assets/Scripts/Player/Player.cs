using UnityEngine;
using System;

namespace TowerDefense
{
    public class Player : MonoSingleton<Player>
    {
        [SerializeField] private SpaceShip m_Ship, m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private int m_NumLives;
        public int NumLives => m_NumLives;

        public event Action OnPlayerDead;

        private void Start()
        {
            if (m_Ship)
            {
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }
        }

        protected void TakeDamage(int damage)
        {
            m_NumLives -= damage;

            if (m_NumLives <= 0)
            {
                m_NumLives = 0;
                OnPlayerDead?.Invoke();
            }
        }

        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPrefab.gameObject);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();

            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

        #region Score (current level only)

        public int Score { get; private set; }

        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}