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

    }

    private void Update()
    {
        transform.LookAt(player);

        if (customer.atBar)
        {
            content.gameObject.SetActive(customer.atBar);
        }

        bool allActive = true;

        for (int i = 0; i < customer.order.items.Count; i++)
        {
            OrderItem item = customer.order.items[i];
            if (!item.has) { allActive = false; }

            GameObject hasGO;
            if (itemHas.TryGetValue(i.ToString(), out hasGO))
            {
                hasGO.SetActive(item.has);
            }
        }

        if (allActive)
        {
            customer.serviceComplete = true;
            customer.happiness = 1;
            MoneySystem.AddMoney(customer.order.cost);
            Destroy(gameObject);
        }
    }

    public bool PickupItem(string itemID)
    {
        for (int i = 0; i < customer.order.items.Count; i++)
        {
            OrderItem item = customer.order.items[i];
            if (item.item == itemID && !item.has)
            {
                item.has = true;
                return true;
            }
        }

        return false;
    }
}
