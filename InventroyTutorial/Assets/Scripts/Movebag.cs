using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movebag : MonoBehaviour, IDragHandler
{
    RectTransform currentRect;
    private void Awake() {
        currentRect=GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition+=eventData.delta;
    }
}
