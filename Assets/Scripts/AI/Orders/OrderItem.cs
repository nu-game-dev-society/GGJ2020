using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderItem
{
    public string item;
    public bool has;
    public readonly Material material;

    public OrderItem(string item, Material material)
    {
        this.item = item;
        this.has = false;
        this.material = material;
    }
}
