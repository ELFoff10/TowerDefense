using UnityEngine;

namespace TowerDefense
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool m_IsCompleted;

        public bool IsCompleted { get { return m_IsCompleted; } }

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                m_IsCompleted = true;
            };
        }
    }
}

