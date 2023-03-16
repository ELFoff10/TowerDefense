using UnityEngine;

namespace TowerDefense
{
    public abstract class Spawner : MonoBehaviour
    {
        protected abstract GameObject GenerateSpawnedEntity();

        [SerializeField] private CircleArea m_CircleArea;

        public enum SpawnMode
        {
            Start, Loop
        }

        [SerializeField] private SpawnMode m_SpawnMode;

        [SerializeField] private int m_NumSpawns;

        [SerializeField] private float m_RespawnTime;

        [SerializeField] private int m_TimeToArmageddon = 75;

        private float m_TimerAddNumSpawns;
        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }
        }

        private void Update()
        {
            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }

            m_TimerAddNumSpawns += Time.deltaTime;

            if (m_TimerAddNumSpawns >= m_TimeToArmageddon)
            {
                m_NumSpawns = 10; // можно сделать серилизованные 10
            }

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for(int i = 0; i < m_NumSpawns; i++)
            {
                var entity = GenerateSpawnedEntity();
                entity.transform.position = m_CircleArea.RandomInsideZone;
            }
        }
    }
}