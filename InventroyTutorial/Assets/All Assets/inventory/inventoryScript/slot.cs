using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public int slotID;//空格ID=空格ID


    public item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotinfo;
    public Text slotinfo2;
    public GameObject itemInslot;
    // Start is called before the first frame update
    public void ItemOnclick()
    {
        InventoryManager.updatItemInfo(slotinfo);
    }
    public void SetupSlot(item item)
    {
        if(item==null)
        {
            itemInslot.SetActive(false);
            return;
        }
        slotImage.sprite=item.itemImage;
        slotNum.text=item.itemHeld.ToString();
        slotinfo=item.itemInfo;
        slotinfo2.text=item.itemInfo;

    }
    
}
