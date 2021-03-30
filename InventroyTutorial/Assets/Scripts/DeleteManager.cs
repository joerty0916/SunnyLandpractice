using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteManager : MonoBehaviour
{
    public Inventory mybag;
    public int slotID;
    public item thisItem;

    // Start is called before the first frame update

    public void setupslotID(int slotID)
    {
        this.slotID=slotID;
    }
    public void OnclickDelete()
    {
        mybag.itemlist[slotID].itemHeld=1;
        mybag.itemlist[slotID]=null;
        InventoryManager.RefreshItem();
        gameObject.SetActive(false);
    }
    public void OnclickCancel()
    {
        gameObject.SetActive(false);
    }

}
