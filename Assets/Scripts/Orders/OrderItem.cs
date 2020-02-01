using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderItem
{
    public string item;
    public int quantity;
    public bool has;

    public OrderItem(string item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
        this.has = false;
    }
}
