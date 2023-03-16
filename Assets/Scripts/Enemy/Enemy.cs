using UnityEngine;
using UnityEditor;
using System;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    [RequireComponent(typeof(Destructible))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType { Base = 0, Magic = 1 }

        /// <summary>
        /// Массив функций, где мы прописали 2 функции.
        /// Func - похожь на Action, только последний параметр яв-ся возвращаемый тип значения
        /// </summary>
        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power, TDProjectile.DamageType type, int armor) =>
            {// ArmorType.Base
                switch (type) // c помощью Base = 0, Magic = 1 мы дергаем функцию, которая нам нужна type == 0, идем сюда
                {
                    case TDProjectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },

            (int power, TDProjectile.DamageType type, int armor) =>
            {// ArmorType.Magic
                if (TDProjectile.DamageType.Base == type)
                {
                    armor = armor / 2;
                }
                return Mathf.Max(power - armor, 1);
            },
        };

        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;
        [SerializeField] private int m_Armor = 1;
        [SerializeField] private ArmorType m_ArmorType;

        private Destructible m_Destructible;

        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();
        }

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
            enemyAsset.Armor = m_Armor;
            enemyAsset.ArmorType = m_ArmorType;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }

        public void GiveGoldPlayer()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }

        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            m_Destructible.ApplyDamage(ArmorDamageFunctions[(int)m_ArmorType](damage, damageType, m_Armor));
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

