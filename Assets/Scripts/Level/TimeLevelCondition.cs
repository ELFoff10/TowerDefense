using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float m_TimeLimit = 25f;

        private void Start()
        {
            m_TimeLimit += Time.time;
        }
        public bool IsCompleted => Time.time > m_TimeLimit;
    }
}

