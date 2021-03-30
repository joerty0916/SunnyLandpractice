using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemOnDrap : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Transform originalParent;
    public Inventory mybag;
    public int currentItemID;//當前物品ID
    public GameObject Narrate;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent=transform.parent;//儲存當前位子
        currentItemID=originalParent.GetComponent<slot>().slotID;
        transform.SetParent(transform.parent.parent);
       transform.position=eventData.position;
       GetComponent<CanvasGroup>().blocksRaycasts=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=eventData.position;
        // Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//輸出滑鼠當前位置道地一個碰到物體名子
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject==null)
        {
            
            GameObject.Find("Canvas/Bag/Delete").SetActive(true);
            GameObject.Find("Canvas/Bag/Delete").GetComponent<DeleteManager>().setupslotID(currentItemID);
        }
        if(eventData.pointerCurrentRaycast.gameObject!=null){
        if(eventData.pointerCurrentRaycast.gameObject.name=="Image")//判斷碰到物體名子
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position=eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            //itemlist的物品存儲位置改變
            var temp=mybag.itemlist[currentItemID];
            mybag.itemlist[currentItemID]=mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotID];
            mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotID]=temp;


            eventData.pointerCurrentRaycast.gameObject.transform.parent.position=originalParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
            GetComponent<CanvasGroup>().blocksRaycasts=true;
            return;
        }
        if(eventData.pointerCurrentRaycast.gameObject.name=="slot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position=eventData.pointerCurrentRaycast.gameObject.transform.position;

            mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotID]=mybag.itemlist[currentItemID];

            if(eventData.pointerCurrentRaycast.gameObject.GetComponent<slot>().slotID!=currentItemID)
            {
                mybag.itemlist[currentItemID]=null;
            }
                    
            GetComponent<CanvasGroup>().blocksRaycasts=true;
            return;
            }
        }
        transform.SetParent(originalParent);
        transform.position=originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts=true;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(eventData.pointerCurrentRaycast.gameObject.name=="Image")
       {
           Narrate.SetActive(true);
       }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject.name!="Image")
       {
           Narrate.SetActive(false);
       }
    }
    
}
