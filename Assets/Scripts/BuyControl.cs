using UnityEngine;

public class BuyControl : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        BuildSite.OnClickEvent += MoveToTransform;
        gameObject.SetActive(false);
    }

    private void MoveToTransform(Transform target)
    {
        // 700, 700
        if (target)
        {
            var posotion = Camera.main.WorldToScreenPoint(target.position);
            m_RectTransform.anchoredPosition = posotion;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
