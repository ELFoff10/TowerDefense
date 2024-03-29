using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemySpawn;

        [SerializeField] private Enemy m_EnemyPrefabs;
        [SerializeField] private Path[] m_Paths;
        [SerializeField] private EnemyWave m_CurrentWave;

        private int m_ActiveEnemyCount = 0;

        public event Action OnAllWavesDead;

        private void Start()
        {
            m_CurrentWave.Prepare(SpawnEnemies);
        }

        public void ForceNextWave()
        {
            if (m_CurrentWave)
            {
                TDPlayer.Instance.ChangeGold((int)m_CurrentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if (m_ActiveEnemyCount == 0)
                {
                    OnAllWavesDead?.Invoke();
                }
            }
        }

        private void RecordEnemyDead()
        {
            if (--m_ActiveEnemyCount == 0)
            {
                ForceNextWave();
            }
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in m_CurrentWave.EnumerateSquads())
            {
                if (pathIndex < m_Paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var enemy = Instantiate(m_EnemyPrefabs, m_Paths[pathIndex].StartArea.RandomInsideZone, Quaternion.identity);
                        enemy.OnEnemyEnd += RecordEnemyDead;
                        enemy.UseEnemyAsset(asset);
                        enemy.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);
                        m_ActiveEnemyCount++;
                        OnEnemySpawn?.Invoke(enemy);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }
    }
}

