using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] BuildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers)
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                BuildableTowers = towers;
            }
        }

        public static event Action<BuildSite> OnClickEvent;

        public static void HideControls()
        {
            OnClickEvent?.Invoke(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }
}