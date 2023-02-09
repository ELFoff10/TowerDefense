using UnityEngine;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_Path;
        private int m_PathIndex;
        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            m_PathIndex = 0;
            SetPatrolBehaviour(m_Path[m_PathIndex]);
        }

        protected override void GetNewPoint()
        {
            m_PathIndex++;

            if (m_Path.Lenght > m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

