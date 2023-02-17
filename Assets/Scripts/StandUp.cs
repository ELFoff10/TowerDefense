using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D m_Rigidbody2D;
        private SpriteRenderer m_SpriteRenderer;

        private void Start()
        {
            m_Rigidbody2D = transform.root.GetComponent<Rigidbody2D>();

            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;

            var xMotion = m_Rigidbody2D.velocity.x;

            if (xMotion > 0.01f)
            {
                m_SpriteRenderer.flipX = false;
            }
            else if (xMotion < 0.01f)
            {
                m_SpriteRenderer.flipX = true;
            }            
        }
    }
}

