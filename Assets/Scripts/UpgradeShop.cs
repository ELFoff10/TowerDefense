using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private BuyUpgrade[] m_Sales;

        private int money;

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
            money = MapCompletion.Instance.TotalScore;
            money -= Upgrades.GetTotalCost();

            moneyText.text = money.ToString();

            foreach (var slot in m_Sales)
            {
                slot.CheckCost(money);
            }
        }
    }
}

