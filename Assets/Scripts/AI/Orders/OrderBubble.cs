using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderBubble : Bubble
{
    [SerializeField] Material tickMaterial;
    [SerializeField] MeshRenderer leftTick, middleTick, rightTick;

    private void Awake()
    {
        leftTick.material = tickMaterial;
        middleTick.material = tickMaterial;
        rightTick.material = tickMaterial;
        //Default to empty
        Clear();
    }

    public void DrawTicks(List<bool> ticks)
    {
        switch(ticks.Count)
        {
            case 1: //Draw in middle
                SetMiddle(ticks[0]);
                break;
            case 2: //Draw either side
                SetLeft(ticks[0]);
                SetRight(ticks[1]);
                break;
            case 3: //Draw in all 3 spots
                SetLeft(ticks[0]);
                SetMiddle(ticks[1]);
                SetRight(ticks[2]);
                break;
            default:
                Debug.Log("Too many/few ticks in bubble");
                break;
        }
    }

    void SetTicked(MeshRenderer renderer, bool ticked) => renderer.enabled = ticked;
    void SetLeft(bool ticked) => SetTicked(leftTick, ticked);
    void SetMiddle(bool ticked) => SetTicked(middleTick, ticked);
    void SetRight(bool ticked) => SetTicked(rightTick, ticked);

    public new void Clear()
    {
        base.Clear();
        SetLeft(false);
        SetMiddle(false);
        SetRight(false);
    }
}

//OLD ORDER BUBBLE
/*
public class OrderBubble : MonoBehaviour
{
    /*
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
*/