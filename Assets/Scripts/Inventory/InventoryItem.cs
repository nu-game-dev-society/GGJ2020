using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string id;
    public string name;
    public ItemType type;
    
    public void Use()
    {
        Debug.Log("Used " + name + " (" + id  + ")");
    }
}

public enum ItemType
{
    Tool,
    Glass,
    Drink,
    Other
}