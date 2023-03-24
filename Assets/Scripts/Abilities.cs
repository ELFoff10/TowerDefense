using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    public class Abilities : MonoSingleton<Abilities>
    {
        [SerializeField] private Button m_SlowButton;
        [SerializeField] private Button m_FireButton;

        //[SerializeField] private Image m_TargetingCircle;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private SlowAbility m_SlowAbility;
        public void UseSlowAbility() => m_SlowAbility.Use();

        private void Start()
        {
            m_FireAbility.Initialization();
            m_SlowAbility.Initialization();
        }

        public void UpgradeFireAbility(int level)
        {
            m_FireAbility.Upgrade(level);
        }
        public void UpgradeSlowAbility(int level)
        {
            m_SlowAbility.Upgrade(level);
        }

        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost;
            [SerializeField] private int m_Damage;
            [SerializeField] private TextMeshProUGUI m_TextCost;
            [SerializeField] private TextMeshProUGUI m_TextDamage;

            public void Initialization()
            {
                TDPlayer.Instance.EnergyUpdateSubscribe(ChekEnergyStatus);
                m_TextCost.text = m_Cost.ToString();
                m_TextDamage.text = m_Damage.ToString();
            }

            public void Upgrade(int level)
            {
                m_Damage += level + 1;
                m_Cost -= level + 1;
                if (m_Cost <= 0)
                {
                    m_Cost = 0;
                }
            }

            public void Use()
            {
                if (TDPlayer.Instance.AbilityEnergy >= m_Cost)
                {
                    ClickProtection.Instance.Activate((Vector2 vector2) =>
                    {
                        Vector3 position = vector2;
                        position.z = -Camera.main.transform.position.z;
                        position = Camera.main.ScreenToWorldPoint(vector2);

                        foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                        {
                            if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                            {
                                enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                            }
                        }
                    });

                    TDPlayer.Instance.ChangeEnergy(-m_Cost);
                }                
            }

            public void ChekEnergyStatus(int energy)
            {
                if (energy < m_Cost)
                {
                    Instance.m_FireButton.interactable = false;
                    m_TextCost.color = Color.red;
                    m_TextDamage.color = Color.red;
                }
                else
                {
                    Instance.m_FireButton.interactable = true;
                    m_TextCost.color = Color.white;
                    m_TextDamage.color = Color.white;
                }
            }
        }

        [Serializable]
        public class SlowAbility
        {
            [SerializeField] private int m_Cost;
            [SerializeField] private float m_CoolDown;
            [SerializeField] private float m_Duration;
            [SerializeField] private TextMeshProUGUI m_TextCoolDown;
            [SerializeField] private TextMeshProUGUI m_TextCost;
            [SerializeField] private TextMeshProUGUI m_TextDuration;

            public void Initialization()
            {
                TDPlayer.Instance.EnergyUpdateSubscribe(ChekEnergyStatus);
                m_TextCoolDown.text = m_CoolDown.ToString();
                m_TextCost.text = m_Cost.ToString();
                m_TextDuration.text = m_Duration.ToString();
            }

            public void Upgrade(int level)
            {
                m_Duration += level + 1;
                m_CoolDown -= level + 1;
                m_Cost -= level + 1;
                if (m_Cost < 0) { m_Cost = 0; }
            }
            public void Use()
            {

                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                if (TDPlayer.Instance.AbilityEnergy >= m_Cost)
                {
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.HalfMaxLinearVelocity();
                    }

                    EnemyWaveManager.OnEnemySpawn += Slow;

                    IEnumerator Restore()
                    {
                        yield return new WaitForSeconds(m_Duration);

                        foreach (var ship in FindObjectsOfType<SpaceShip>())
                        {
                            ship.RestoreMaxLinearVelocity();
                        }

                        EnemyWaveManager.OnEnemySpawn -= Slow;
                    }

                    Instance.StartCoroutine(Restore());

                    TDPlayer.Instance.ChangeEnergy(-m_Cost);

                    Instance.StartCoroutine(TimeAbilityButton());
                }                
            }

            public void ChekEnergyStatus(int energy)
            {
                if (energy < m_Cost)
                {
                    Instance.m_SlowButton.interactable = false;
                    m_TextCoolDown.color = Color.red;
                    m_TextCost.color = Color.red;
                    m_TextDuration.color = Color.red;
                }
                else
                {
                    Instance.m_SlowButton.interactable = true;
                    m_TextCoolDown.color = Color.black;
                    m_TextCost.color = Color.black;
                    m_TextDuration.color = Color.black;
                }
            }

            IEnumerator TimeAbilityButton()
            {
                Instance.m_SlowButton.interactable = false;
                yield return new WaitForSeconds(m_CoolDown);
                Instance.m_SlowButton.interactable = true;
            }
        }
    }
}

