using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int m_LevelScore = 3;

        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(
                () =>
                {
                    StopLevelActivity();

                    if (m_ReferenceTime <= Time.time)
                    {
                        m_LevelScore -= 1;
                    }
                    
                    MapCompletion.SaveEpisodeResult(m_LevelScore); // ����� ���� ����� ��������� � LevSeqCon
                }
            );

            void LifeScoreChange(int _)
            {
                m_LevelScore -= 1;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            }

            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
        }
    }
}
