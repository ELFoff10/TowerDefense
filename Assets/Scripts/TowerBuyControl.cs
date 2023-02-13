using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {

        [SerializeField] private TowerAsset m_TowerAsset;
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform m_BuildSite;
        public Transform BuildSite { set { m_BuildSite = value; } }

        private void Start()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_TowerAsset.GoldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_TowerAsset.GoldCost != m_Button.interactable) // �� ����� 22.5 ������� ������� ����� ����� 30 ������
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
        }
    }
}
