using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject customerPrefab;

    [SerializeField]
    private float spawnTimeMin = 5.0f;

    [SerializeField]
    private float spawnTimeMax = 10.0f;

    private int maxItems = 2;

    [SerializeField]
    private List<OrderRand> orderRandItems;

    private float lastSpawn;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastSpawn) >= spawnTime)
        {
            lastSpawn = Time.time;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax + 1);

            GameObject customer = GameObject.Instantiate(customerPrefab);
            customer.transform.position = transform.position;

            List<OrderItem> orderItems = new List<OrderItem>();

            while (orderItems.Count <= 0 || orderItems.Count > maxItems)
            {
                orderItems.Clear();

                foreach (OrderRand order in orderRandItems)
                {
                    int count = Random.Range(order.min, order.max + 1);
                    Debug.Log("Adding " + order.id + " x" + count);
                    
                    for (int i = 0; i < count; i++)
                    {
                        orderItems.Add(new OrderItem(order.id));
                    }
                }
            }

            Debug.Log("Adding " + orderItems.Count);
            Customer cust = customer.GetComponent<Customer>();
            cust.order = new Order(orderItems);
            OrderManager.Instance.CreateBubble(cust, cust.order);
        }
    }
}
