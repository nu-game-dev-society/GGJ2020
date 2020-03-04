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

    public int[] GenerateOrder(CustomerController cust)
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        List<int> orderIDs = new List<int>();
        int orderCost = 0;
        
        for(int i = 0; i<maxItems; i++)
        {
            int rand = Random.Range(0, possibleItems.Count+i);
            if (rand >= possibleItems.Count)
                continue;
            OrderPossibleItem opi = possibleItems[rand];
            orderCost += possibleItems[rand].price;
            orderItems.Add(new OrderItem(opi.id, opi.material));
            orderIDs.Add(rand);
        }
        Order o = new Order(orderItems, orderCost);
        cust.SetOrder(o);
        return orderIDs.ToArray();
    }
    public void GenerateOrder(CustomerController cust, int[] orderIDs)
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        int orderCost = 0;

        foreach (int i in orderIDs)
        {
            OrderPossibleItem opi = possibleItems[i];
            orderCost += possibleItems[i].price;
            orderItems.Add(new OrderItem(opi.id, opi.material));
        }
        cust.SetOrder(new Order(orderItems, orderCost));
    }
}
