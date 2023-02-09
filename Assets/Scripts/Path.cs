using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private AIPointPatrol[] m_Points;
        public int Lenght { get { return m_Points.Length; } }

        public AIPointPatrol this[int index] { get => m_Points[index]; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = UnityEngine.Color.green;

            foreach (var point in m_Points)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
            }
        }
    }
}

