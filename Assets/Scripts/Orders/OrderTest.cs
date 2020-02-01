using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTest : MonoBehaviour
{
    private Customer customer;
    private Order order;

    void Start()
    {
        customer = GetComponent<Customer>();
        order = new Order(new List<OrderItem> { new OrderItem("wine") });

        OrderManager.Instance.CreateBubble(customer, order);
    }
}
