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

    public OrderImage[] images;
    public Sprite defaultImage;

    private int maxItems = 2;

    [SerializeField]
    private List<OrderRand> orderRandItems;

    public void CreateBubble(CustomerController customer, Order order)
    {
        GameObject bubble = GameObject.Instantiate(bubblePrefab);
        bubble.transform.SetParent(customer.transform);
        bubble.transform.position = customer.transform.Find("BubblePos").position;
        bubble.GetComponent<OrderBubble>().order = order;
    }

    public Sprite GetItemImage(string itemID)
    {
        foreach (OrderImage image in images)
        {
            if (itemID == image.itemID)
            {
                return image.itemImage;
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

        while (orderItems.Count <= 0 || orderItems.Count > maxItems)
        {
            orderItems.Clear();

            foreach (OrderRand order in orderRandItems)
            {
                int count = Random.Range(order.min, order.max + 1);
                //Debug.Log("Adding " + order.id + " x" + count);

                for (int i = 0; i < count; i++)
                {
                    orderItems.Add(new OrderItem(order.id));
                }
            }
        }

        //Debug.Log("Adding " + orderItems.Count);

        cust.order = new Order(orderItems);
        CreateBubble(cust, cust.order);
    }
}
