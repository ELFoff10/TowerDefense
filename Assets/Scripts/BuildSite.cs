using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public static event Action<Transform> OnClickEvent;
    protected void InvokeNullEvent()
    {
        OnClickEvent?.Invoke(null);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(transform.root);
    }
}
