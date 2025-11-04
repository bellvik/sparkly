using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrowButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 0.9f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
