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
        //orderItems.Add(new OrderItem(possibleItems[0].id, possibleItems[0].material));
        //orderItems.Add(new OrderItem(possibleItems[0].id, possibleItems[0].material));
        int orderCost = 0;
        
        for(int i = 0; i<maxItems; i++)
        {
            int rand = Random.Range(0, possibleItems.Count+i);
            if (rand >= possibleItems.Count)
                continue;
            OrderPossibleItem opi = possibleItems[rand];
            orderCost += possibleItems[rand].price;
            orderItems.Add(new OrderItem(opi.id, opi.material));
        }
        cust.SetOrder(new Order(orderItems, orderCost));
    }
}
