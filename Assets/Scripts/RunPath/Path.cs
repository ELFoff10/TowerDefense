using UnityEngine;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private AIPointPatrol[] m_Points;
        [SerializeField] private CircleArea m_StartArea;
        public CircleArea StartArea => m_StartArea;
        public int Lenght { get { return m_Points.Length; } }

        public AIPointPatrol this [int index] { get => m_Points[index]; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach (var point in m_Points)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
            }
        }
    }
}

