using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Order
{
    public List<OrderItem> items;
    public int cost;

    public Order(List<OrderItem> items, int cost)
    {
        this.items = items;
        this.cost = cost;
    }
}
