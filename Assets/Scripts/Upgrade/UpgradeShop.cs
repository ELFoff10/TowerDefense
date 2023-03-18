using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_TextAmountHearts;
        [SerializeField] private BuyUpgrade[] m_Sales;

        private int m_Money;

        private void Start()
        {
            foreach (var slot in m_Sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick
                    .AddListener(UpdateMoney);
            }

            UpdateMoney();
        }

        public void UpdateMoney()
        {
            m_Money = MapCompletion.Instance.TotalScore;
            m_Money -= Upgrades.GetTotalCost();

            m_TextAmountHearts.text = m_Money.ToString();

            foreach (var slot in m_Sales)
            {
                slot.CheckCost(m_Money);
            }
        }
    }
}

