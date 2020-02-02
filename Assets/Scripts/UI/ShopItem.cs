using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public string id;
    public string name;
    public string desc;
    public int price;
    public int ownedNum;
    public int maxNum;
    public Sprite image;
    public GameObject go;
}
