using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderBubble : MonoBehaviour
{
    private Dictionary<string, GameObject> itemHas = new Dictionary<string, GameObject>();

    private Transform player;

    private CustomerController customer;

    [SerializeField]
    private Transform content;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        customer = GetComponentInParent<CustomerController>();

        Vector3 pos1 = content.position;
        Vector3 pos2 = content.position;

        if (customer.order.items.Count == 2)
        {
            pos1 = pos1 - (content.right * 0.3f);
            pos2 = pos2 + (content.right * 0.3f);

            GameObject itemGO2 = GameObject.Instantiate(OrderManager.Instance.itemPrefab);
            itemGO2.transform.SetParent(content);
            itemGO2.transform.position = pos2;
            itemGO2.transform.rotation = content.rotation;

            itemGO2.transform.Find("Image").GetComponent<MeshRenderer>().material = OrderManager.Instance.GetItemImageMat(customer.order.items[1].item);

            itemHas.Add("1", itemGO2.transform.Find("Has").gameObject);
        }
        else
        {
            pos1 = pos1 - (content.right * 0.05f);
        }

        GameObject itemGO1 = GameObject.Instantiate(OrderManager.Instance.itemPrefab);
        itemGO1.transform.SetParent(content);
        itemGO1.transform.position = pos1;
        itemGO1.transform.rotation = content.rotation;

        itemGO1.transform.Find("Image").GetComponent<MeshRenderer>().material = OrderManager.Instance.GetItemImageMat(customer.order.items[0].item);

        itemHas.Add("0", itemGO1.transform.Find("Has").gameObject);

        content.gameObject.SetActive(customer.atBar);
    }

    void FixedUpdate()
    {
        bool allActive = true;

        foreach (OrderItem item in customer.order.items)
        {
            if (!item.has) { allActive = false; }

            GameObject hasGO;
            if (itemHas.TryGetValue(item.item, out hasGO))
            {
                hasGO.SetActive(item.has);
            }
        }

        if (allActive)
        {
            customer.serviceComplete = true;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.LookAt(player);

        if (customer.atBar)
        {
            content.gameObject.SetActive(customer.atBar);
        }
    }

    public bool PickupItem(string itemID)
    {
        foreach (OrderItem item in customer.order.items)
        {
            if (item.item == itemID)
            {
                item.has = true;
                return true;
            }
        }

        return false;
    }
}
