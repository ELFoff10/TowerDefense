using UnityEngine;
using UnityEngine.XR;

namespace TowerDefense
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretProperties m_TurretProperties;


        private TurretMode m_TurretMode;
        public TurretMode TurretMode => m_TurretMode;

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

            if (m_TurretProperties == null)
                return;

            // инстанцируем прожектайл который уже сам полетит.
            var projectile = Instantiate(m_TurretProperties.ProjectilePrefab.gameObject).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            // метод выставления данных прожектайлу о том кто стрелял для избавления от попаданий в самого себя
            projectile.SetParentShooter();

            m_RefireTimer = m_TurretProperties.RateOfFire;
        }

        public void AssignLoadOut(TurretProperties turretProperties)
        {
            m_RefireTimer = 0;
            m_TurretProperties = turretProperties;
            m_TurretMode = turretProperties.Mode;
        }
    }
}