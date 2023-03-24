using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_UpgradeAsset;
        [SerializeField] private TextMeshProUGUI m_TextLevel, m_TextCost;
        [SerializeField] private Button m_ButtonBuy;

        private int m_CostNumber = 0;

        public void Initialize()
        {
            var savedLevel = Upgrades.GetUpgradeLevel(m_UpgradeAsset);
            m_TextLevel.text = (savedLevel).ToString();
            m_TextCost.text = m_UpgradeAsset.CostByLevel[savedLevel].ToString();

            if (savedLevel >= m_UpgradeAsset.CostByLevel.Length)
            {
                m_ButtonBuy.interactable = false;
                //m_BuyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                //m_BuyButton.transform.Find("Text").gameObject.SetActive(false);
                //m_TextCost.text = "X";
                m_CostNumber = int.MaxValue;
            }
            else
            {
                m_ButtonBuy.interactable = true;
                m_CostNumber = m_UpgradeAsset.CostByLevel[savedLevel];
            }
        }

        public void CheckCost(int money)
        {
            m_ButtonBuy.interactable = money >= m_CostNumber;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(m_UpgradeAsset);
            Initialize();
        }
    }
}

