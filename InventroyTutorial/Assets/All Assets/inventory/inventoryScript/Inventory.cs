using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Inventory",menuName="Inventory/New Inventory")]
public class Inventory : ScriptableObject

{
    // Start is called before the first frame update
   public List<item> itemlist=new List<item>();
}
