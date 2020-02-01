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

            OrderManager.Instance.GenerateOrder(customer.GetComponent<CustomerController>());
        }
    }
}
