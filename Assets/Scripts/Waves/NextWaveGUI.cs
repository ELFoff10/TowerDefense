using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_BonusAmount;

        private EnemyWaveManager m_Manager;
        private float m_TimeToNextWave;

        private void Start()
        {
            m_Manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                m_TimeToNextWave = time;
            };
        }

        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }

        private void Update()
        {
            var bonus = (int)m_TimeToNextWave;
            if (bonus < 0) bonus = 0;
            m_BonusAmount.text = bonus.ToString();
            m_TimeToNextWave -= Time.deltaTime;
        }
    }
}

