using UnityEngine;
using UnityEditor;
using System;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;

        public event Action OnEnemyEnd;
        private void OnDestroy()
        {
            OnEnemyEnd?.Invoke();
        }

        public void UseEnemyAsset(EnemyAsset enemyAsset)
        {
            var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            spriteRenderer.color = enemyAsset.Color;

            spriteRenderer.transform.localScale = new Vector3(enemyAsset.SpriteScale.x, enemyAsset.SpriteScale.y, 1);

            spriteRenderer.GetComponent<Animator>().runtimeAnimatorController = enemyAsset.Animations;

            GetComponent<SpaceShip>().Use(enemyAsset);

            GetComponentInChildren<CircleCollider2D>().radius = enemyAsset.Radius;

            enemyAsset.Damage = m_Damage;
            enemyAsset.Gold = m_Gold;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }

        public void GiveGoldPlayer()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
    }

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if (a)
            {
                (target as Enemy).UseEnemyAsset(a);
            }
        }
    }
}

