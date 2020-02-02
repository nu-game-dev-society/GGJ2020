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

    public GameObject bubblePrefab;
    public GameObject itemPrefab;

    [SerializeField]
    private Material baseMaterial;

    public Sprite defaultImage;

    private int maxItems = 2;

    [SerializeField]
    private List<OrderPossibleItem> orderPossibleItems;

    public void CreateBubble(CustomerController customer)
    {
        GameObject bubble = GameObject.Instantiate(bubblePrefab);
        bubble.transform.SetParent(customer.transform);
        bubble.transform.position = customer.transform.Find("BubblePos").position;
    }

    public Sprite GetItemImage(string itemID)
    {
        foreach (OrderPossibleItem possibleItem in orderPossibleItems)
        {
            if (itemID == possibleItem.id)
            {
                return possibleItem.itemImage;
            }
        }

        return defaultImage;
    }

    public Material GetItemImageMat(string itemID)
    {
        Sprite image = GetItemImage(itemID);

        Material material = new Material(baseMaterial);
        material.SetTexture("_MainTex", image.texture);

        return material;
    }

    public void GenerateOrder(CustomerController cust)
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        int orderCost = 0;

        while (orderItems.Count <= 0 || orderItems.Count > maxItems)
        {
            orderItems.Clear();
            orderCost = 0;

            foreach (OrderPossibleItem possibleItem in orderPossibleItems)
            {
                int count = Random.Range(possibleItem.min, possibleItem.max + 1);

                for (int i = 0; i < count; i++)
                {
                    orderCost += possibleItem.price;
                    orderItems.Add(new OrderItem(possibleItem.id));
                }
            }
        }

        cust.order = new Order(orderItems, orderCost);
        CreateBubble(cust);
    }
}
