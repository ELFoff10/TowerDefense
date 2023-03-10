using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] m_Levels;
        [SerializeField] private BranchLevel[] m_BranchLevel;

        private void Start()
        {
            var drawLevel = 0;
            var score = 1;

            while (score != 0 && drawLevel < m_Levels.Length &&
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                m_Levels[drawLevel].SetLevelData(episode, score);
                drawLevel++;
            }

            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                m_Levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < m_BranchLevel.Length; i++)
            {
                m_BranchLevel[i].TryActivate();
            }
        }
    }
}

