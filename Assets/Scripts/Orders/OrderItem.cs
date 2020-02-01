using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderItem
{
    public string item;
    public bool has;

    public OrderItem(string item)
    {
        this.item = item;
        this.has = false;
    }
}
