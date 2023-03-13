using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_TowerAsset;
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform m_BuildSite;

        public void SetBuildSite(Transform value)
        {
            m_BuildSite = value;
        }

        private void Start()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_TowerAsset.GoldCost.ToString();
            //m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
        }

        private void OnDestroy()
        {
            TDPlayer.GoldUpdateUnSubscribe(GoldStatusCheck);
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_TowerAsset.GoldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
            BuildSite.HideControls();
        }
    }
}

