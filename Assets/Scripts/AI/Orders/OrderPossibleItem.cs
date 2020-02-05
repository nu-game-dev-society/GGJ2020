using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderPossibleItem
{
    public string id;
    public int min;
    public int max;
    public Sprite itemImage;
    public Material material;
    public int price;
}
