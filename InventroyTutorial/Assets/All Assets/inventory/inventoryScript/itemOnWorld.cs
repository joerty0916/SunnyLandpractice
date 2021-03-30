using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    // Start is called before the first frame update
    public item thisItem;
    public Inventory playerInventory;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }  
    }
    public void AddNewItem()
    {
        if(!playerInventory.itemlist.Contains(thisItem))
        {
            // playerInventory.itemlist.Add(thisItem);
            // InventoryManager.CreateNewItem(thisItem);
            for (int i = 0; i < playerInventory.itemlist.Count; i++)
            {
                if(playerInventory.itemlist[i]==null)
                {
                    playerInventory.itemlist[i]=thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemHeld+=1;
        }
        InventoryManager.RefreshItem();
    }
}
