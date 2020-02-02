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

    [SerializeField]
    private int maxCustomers = 20;

    [SerializeField]
    private Color[] tshirtColours;

    [SerializeField]
    private Color[] trouserColours;

    [SerializeField]
    private Color[] hairColours;

    [SerializeField]
    private Color[] skinColours;

    private float lastSpawn;
    private float spawnTime;

    private MaterialPropertyBlock propBlock;

    private List<CustomerController> activeCustomers;
    private List<CustomerController> inactiveCustomers;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        propBlock = new MaterialPropertyBlock();
        activeCustomers = new List<CustomerController>();
        inactiveCustomers = new List<CustomerController>();

        if (tshirtColours.Length == 0)
            tshirtColours = new Color[] { new Color(204.0f / 255.0f, 185.0f / 255.0f, 99.0f / 255.0f) };

        if (trouserColours.Length == 0)
            trouserColours = new Color[] { new Color(17.0f / 255.0f, 47.0f / 255.0f, 81.0f / 255.0f) };

        if (hairColours.Length == 0)
            hairColours = new Color[] { new Color(18.0f / 255.0f, 10.0f / 255.0f, 5.0f / 255.0f) };

        if (skinColours.Length == 0)
            skinColours = new Color[] { new Color(204.0f / 255.0f, 152.0f / 255.0f, 194.0f / 255.0f) };
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastSpawn) >= spawnTime)
        {
            lastSpawn = Time.time;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax + 1);

            if ((activeCustomers.Count + inactiveCustomers.Count) >= maxCustomers) { return; }

            GameObject customer;
            CustomerController customerController;

            if (inactiveCustomers.Count >= 1)
            {
                customerController = inactiveCustomers[0];
                customer = customerController.gameObject;
                inactiveCustomers.Remove(customerController);

                customerController.JoinQueue();
            }
            else
            {
                customer = GameObject.Instantiate(customerPrefab);
                customerController = customer.GetComponent<CustomerController>();
            }

            customer.SetActive(true);

            activeCustomers.Add(customerController);

            customer.transform.position = transform.position;
            foreach (SkinnedMeshRenderer renderer in customer.transform.Find("BaseCustomer@Happy Idle").GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                renderer.GetPropertyBlock(propBlock);

                switch (renderer.gameObject.name)
                {
                    case "HairShort":
                        propBlock.SetColor("_Color", hairColours[Random.Range(0, hairColours.Length)]);
                        break;
                    case "TShirt":
                        propBlock.SetColor("_Color", tshirtColours[Random.Range(0, tshirtColours.Length)]);
                        break;
                    case "Pants":
                        propBlock.SetColor("_Color", trouserColours[Random.Range(0, trouserColours.Length)]);
                        break;
                    case "Male":
                        propBlock.SetColor("_Color", skinColours[Random.Range(0, skinColours.Length)]);
                        break;
                    default:
                        break;
                }

                renderer.SetPropertyBlock(propBlock);
            }

            OrderManager.Instance.GenerateOrder(customerController);
        }
    }

    void PoolCustomer(CustomerController customer)
    {
        activeCustomers.Remove(customer);
        inactiveCustomers.Add(customer);
        customer.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        CustomerController customer = other.GetComponent<CustomerController>();
        if (customer)
        {
            PoolCustomer(customer);
        }
    }
}
