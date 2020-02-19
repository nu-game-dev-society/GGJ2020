using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    #region Singleton Pattern
    public static OrderManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    const int maxItems = 2;

    [SerializeField] List<OrderPossibleItem> possibleItems;

    public void GenerateOrder(CustomerController cust)
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        orderItems.Add(new OrderItem(possibleItems[0].id, possibleItems[0].material));
        orderItems.Add(new OrderItem(possibleItems[0].id, possibleItems[0].material));
        int orderCost = 0;
        /*
        while (orderItems.Count <= 0 || orderItems.Count > maxItems)
        {
            orderItems.Clear();
            orderCost = 0;

            foreach (OrderPossibleItem possibleItem in possibleItems)
            {
                int count = Random.Range(possibleItem.min, possibleItem.max + 1);

                for (int i = 0; i < count; i++)
                {
                    orderCost += possibleItem.price;
                    orderItems.Add(new OrderItem(possibleItem.id, possibleItem.material));
                }
            }
        }*/

        cust.SetOrder(new Order(orderItems, orderCost));
    }
}
