using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_UpgradeAsset;
        [SerializeField] private TextMeshProUGUI level, cost;
        [SerializeField] private Button buyButton;

        private int m_CostNumber = 0;

        public void Initialize()
        {
            //for (int i = 0; i < m_UpgradeAsset.Length; i++)
            //{
            var savedLevel = Upgrades.GetUpgradeLevel(m_UpgradeAsset);
            level.text = (savedLevel + 1).ToString();
            cost.text = m_UpgradeAsset.m_CostByLevel[savedLevel].ToString();

            if (savedLevel >= m_UpgradeAsset.m_CostByLevel.Length)
            {
                buyButton.interactable = false;
                //m_BuyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                //m_BuyButton.transform.Find("Text").gameObject.SetActive(false);
                //m_TextCost.text = "X";
                m_CostNumber = int.MaxValue;
            }
            else
            {
                buyButton.interactable = true;
                m_CostNumber = m_UpgradeAsset.m_CostByLevel[savedLevel];
            }
            //}
        }

        public void CheckCost(int money)
        {
            buyButton.interactable = money >= m_CostNumber;
        }

        public void Buy()
        {
            //for (int i = 0; i < m_UpgradeAsset.Length; i++)
            //{
            Upgrades.BuyUpgrade(m_UpgradeAsset);
            Initialize();
            //}
        }
        //public void BuyMoney()
        //{
        //    Upgrades.BuyUpgrade(m_UpgradeAsset);
        //    Initialize();
        //}

    }
}

