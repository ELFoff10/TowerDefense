using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт прожектайла. Кидается на топ префаба прожектайла.
    /// </summary>
    public class Projectile : Entity
    {
        [SerializeField] private bool isHoming;

        private Destructible m_Target;

        /// <summary>
        /// Линейная скорость полета снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_Lifetime;

        /// <summary>
        /// Повреждения наносимые снарядом.
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// Эффект попадания от что то твердое. 
        /// </summary>
        //[SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = GetDirection() * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);
            
            // не забыть выключить в свойствах проекта, вкладка Physics2D иначе не заработает
            // disable queries hit triggers
            // disable queries start in collider
            if (hit)
            {
                var destructible = hit.collider.transform.root.GetComponent<Destructible>();

                if(destructible != null)
                {
                    destructible.ApplyDamage(m_Damage);

                    // #Score
                    // добавляем очки за уничтожение
                    //if(Player.Instance != null && destructible.HitPoints < 0)
                    //{
                    //    // проверяем что прожектайл принадлежит кораблю игрока. 
                    //    // здесь есть нюанс - если мы выстрелим прожектайл и после умрем
                    //    // то новый корабль игрока будет другим, в случае если прожектайл запущенный из предыдущего шипа
                    //    // добьет то очков не дадут. Можно отправить пофиксить на ДЗ. (например тупо воткнув флаг что прожектайл игрока)
                    //    //if(m_Parent == Player.Instance.ActiveShip)
                    //    //{
                    //    //    Player.Instance.AddScore(destructible.ScoreValue);
                    //    //}
                    //}
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if(m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 pos)
        {
            //if(m_ImpactEffectPrefab != null)
            //{
            //    var impact = Instantiate(m_ImpactEffectPrefab.gameObject);
            //    impact.transform.position = pos;
            //}

            Destroy(gameObject);
        }

        public void SetParentShooter()
        {
            m_Target = FindNearestDestructableTarget();
        }
        private Destructible FindNearestDestructableTarget() // Находит ближайший объект с Destructible
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;

            // Перебираем все разрушаемые объекты сцены.
            foreach (var destructible in Destructible.AllDestructibles)
            {
                // Определем дистанцию до объекта.
                float dist = Vector2.Distance(transform.position, destructible.transform.position);

                // Проверем что эта дистанция короче
                if (dist < maxDist)
                {
                    // Переписываем минимальную дистанцию на ещё более короткую.
                    maxDist = dist;

                    // Запоминаем этого врага (кэш - как наиболее подходящего для атаки).
                    potentialTarget = destructible;
                }
            }

            return potentialTarget;
        }

        private Vector3 GetDirection() // Направление снаряда, нормализованный вектор
        {
            if (isHoming && m_Target != null)
            {
                transform.up = (m_Target.transform.position - transform.position).normalized;
                return (m_Target.transform.position - transform.position).normalized;
            }
            else
            {
                return transform.up;
            }
        }
    }
}

