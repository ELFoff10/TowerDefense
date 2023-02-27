using UnityEngine;

namespace TowerDefense
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_TowerAsset;

        private float m_RefireTimer;
        public bool CanFire => m_RefireTimer <= 0;

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
        }

        public void Fire()
        {
            if (m_RefireTimer > 0)
                return;

            if (m_TowerAsset == null)
                return;

            // инстанцируем прожектайл который уже сам полетит.
            var projectile = Instantiate(m_TowerAsset.ProjectilePrefab.gameObject).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            // метод выставления данных прожектайлу о том кто стрелял для избавления от попаданий в самого себя
            projectile.SetParentShooter();

            m_RefireTimer = m_TowerAsset.RateOfFire;
        }
    }
}