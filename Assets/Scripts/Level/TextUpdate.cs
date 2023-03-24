using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource 
        {
            Gold, Life, Energy
        }

        public UpdateSource Source = UpdateSource.Gold;

        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_Text = GetComponent<TextMeshProUGUI>();

            switch (Source)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Energy:
                    TDPlayer.Instance.EnergyUpdateSubscribe(UpdateText);
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (Source)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateUnSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateUnSubscribe(UpdateText);
                    break;
                case UpdateSource.Energy:
                    TDPlayer.Instance.EnergyUpdateUnSubscribe(UpdateText);
                    break;
            }
        }

        private void UpdateText(int i)
        {
            m_Text.text = i.ToString();
        }
    }
}

