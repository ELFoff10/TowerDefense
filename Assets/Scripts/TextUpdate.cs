using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource 
        {
            Gold, Life
        }

        public UpdateSource Source = UpdateSource.Gold;

        private TextMeshProUGUI m_Text;
        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            switch (Source)
            {
                case UpdateSource.Gold:
                    TDPlayer.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.LifeUpdateSubscribe(UpdateText);
                    break;
            }
        }

        private void UpdateText(int money)
        {
            m_Text.text = money.ToString();
        }
    }
}

