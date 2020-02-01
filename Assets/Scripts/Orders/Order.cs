using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public List<OrderItem> items;

    public Order(List<OrderItem> items)
    {
        this.items = items;
    }
}
