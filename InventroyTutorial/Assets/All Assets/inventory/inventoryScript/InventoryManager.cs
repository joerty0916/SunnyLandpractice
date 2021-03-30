using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{


   static InventoryManager instance;
   public Inventory myBag;
   public GameObject slotGrid;
//    public slot slotPrefab;
    public GameObject emptslot;
   public Text intemInformation;
   public List<GameObject> slots= new List<GameObject>();
   private void Awake() {
       if(instance!=null)
       Destroy(gameObject);
       instance=this;
       RefreshItem();
       
   }
   private void OnEnable() {
    //    RefreshItem();
       instance.intemInformation.text="";
   }
   public static void updatItemInfo(string itemDescription)
   {
       instance.intemInformation.text=itemDescription;
   }
//    public static void CreateNewItem(item item)
//    {
//        slot newItem=Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
//        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
//        newItem.slotItem=item;
//        newItem.slotImage.sprite=item.itemImage;
//        newItem.slotNum.text=item.itemHeld.ToString();
//    }
   public static void RefreshItem()
   {
       instance.slots.Clear();
       for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
       {
           if(instance.slotGrid.transform.childCount==0)
           break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            
       }
       for (int i = 0; i < instance.myBag.itemlist.Count; i++)
       {
        //    CreateNewItem(instance.myBag.itemlist[i]);
        instance.slots.Add(Instantiate(instance.emptslot));
        instance.slots[i].transform.SetParent(instance.slotGrid.transform);
        instance.slots[i].GetComponent<slot>().slotID=i;
        instance.slots[i].GetComponent<slot>().SetupSlot(instance.myBag.itemlist[i]);

       }
   }
}
