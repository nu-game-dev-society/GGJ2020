﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderBubble : MonoBehaviour
{
    public Order order;

    private Dictionary<string, GameObject> itemHas = new Dictionary<string, GameObject>();

    private Transform player;

    [SerializeField]
    private Transform content;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;

        foreach (OrderItem item in order.items)
        {
            GameObject itemGO = GameObject.Instantiate(OrderManager.Instance.itemPrefab);
            itemGO.transform.SetParent(content);
            itemGO.transform.position = content.position;
            itemGO.transform.rotation = content.rotation;

            itemGO.transform.Find("Image").GetComponent<MeshRenderer>().material = OrderManager.Instance.GetItemImageMat(item.item);
            itemGO.transform.Find("Quantity").GetComponent<TextMeshPro>().text = "x" + item.quantity;

            itemHas.Add(item.item, itemGO.transform.Find("Has").gameObject);
        }
    }

    void FixedUpdate()
    {
        foreach (OrderItem item in order.items)
        {
            GameObject hasGO;
            if (itemHas.TryGetValue(item.item, out hasGO))
            {
                hasGO.SetActive(item.has);
            }
        }
    }

    private void Update()
    {
        transform.LookAt(player);
    }
}