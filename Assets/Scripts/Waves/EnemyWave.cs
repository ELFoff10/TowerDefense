using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        [SerializeField] private PathGroup[] m_Groups;

        [SerializeField] private float m_PrepareTime = 10f;

        [SerializeField] private EnemyWave m_NextWave;

        public static Action<float> OnWavePrepare;

        private event Action OnWaveReady;

        [Serializable]
        private class Squad
        {
            public EnemyAsset EnemyAsset;
            public int Count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] Squads;
        }

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time >= m_PrepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        public float GetRemainingTime() { return m_PrepareTime - Time.time; }

        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_PrepareTime);
            m_PrepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (m_NextWave)
            {
                m_NextWave.Prepare(spawnEnemies);
            }
            return m_NextWave;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < m_Groups.Length; i++)
            {
                foreach (var squad in m_Groups[i].Squads)
                {
                    yield return (squad.EnemyAsset, squad.Count, i);
                }                
            }            
        }
    }
}


