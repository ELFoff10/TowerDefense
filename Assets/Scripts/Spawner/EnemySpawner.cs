using UnityEngine;

namespace TowerDefense
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_EnemyPrefabs;
        [SerializeField] private Path m_Path;
        [SerializeField] private EnemyAsset[] m_EnemyAsset;

        protected override GameObject GenerateSpawnedEntity()
        {
            var enemy = Instantiate(m_EnemyPrefabs);
            enemy.UseEnemyAsset(m_EnemyAsset[Random.Range(0, m_EnemyAsset.Length)]);
            enemy.GetComponent<TDPatrolController>().SetPath(m_Path);
            return enemy.gameObject;
        }
    }
}