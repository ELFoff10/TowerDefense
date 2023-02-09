using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {        
        public void Use(EnemyAsset enemyAsset)
        {
            var spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            spriteRenderer.color = enemyAsset.Color;

            spriteRenderer.transform.localScale = new Vector3(enemyAsset.SpriteScale.x, enemyAsset.SpriteScale.y, 1);

            spriteRenderer.GetComponent<Animator>().runtimeAnimatorController = enemyAsset.Animations;

            GetComponent<SpaceShip>().Use(enemyAsset);

            GetComponentInChildren<CircleCollider2D>().radius = enemyAsset.Radius;
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
                (target as Enemy).Use(a);
            }
        }
    }
}

