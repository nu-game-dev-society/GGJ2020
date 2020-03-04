using Photon.Pun;
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

    public static int maxCustomers = 20;

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
    public Waypoint exitWP;
    public static List<CustomerController> activeCustomers = new List<CustomerController>();
    public static List<CustomerController> inactiveCustomers = new List<CustomerController>();
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        propBlock = new MaterialPropertyBlock();

        if (tshirtColours.Length == 0)
            tshirtColours = new Color[] { new Color(204.0f / 255.0f, 185.0f / 255.0f, 99.0f / 255.0f) };

        if (trouserColours.Length == 0)
            trouserColours = new Color[] { new Color(17.0f / 255.0f, 47.0f / 255.0f, 81.0f / 255.0f) };

        if (hairColours.Length == 0)
            hairColours = new Color[] { new Color(18.0f / 255.0f, 10.0f / 255.0f, 5.0f / 255.0f) };

        if (skinColours.Length == 0)
            skinColours = new Color[] { new Color(204.0f / 255.0f, 152.0f / 255.0f, 194.0f / 255.0f) };
        //if (!PhotonNetwork.IsMasterClient)
        //this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        if ((Time.time - lastSpawn) >= spawnTime)
        {
            lastSpawn = Time.time;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax + 1);

            SpawnCustomer();
        }
    }

    private void SpawnCustomer()
    {
        Debug.Log("Spawned");
        GameObject customer;
        CustomerController customerController;

        if (inactiveCustomers.Count >= 1)
        {   //GetFromPool
            customerController = inactiveCustomers[0];
            customer = customerController.gameObject;
            inactiveCustomers.Remove(customerController);
            customerController.serviceComplete = false;

            customer.SetActive(true);
            customerController.JoinQueue();
        }
        else
        {   //Instantiate new Customers
            if ((activeCustomers.Count + inactiveCustomers.Count) >= maxCustomers) { return; }
            customer = Instantiate(customerPrefab);
            customerController = customer.GetComponent<CustomerController>();
        }

        customer.SetActive(true);
        customerController.exitWP = exitWP;
        activeCustomers.Add(customerController);

        customer.transform.position = transform.position;
        int hairColor = Random.Range(0, hairColours.Length);
        int tshirtColor = Random.Range(0, tshirtColours.Length);
        int trouserColor = Random.Range(0, trouserColours.Length);
        int skinColor = Random.Range(0, skinColours.Length);

        SetCustomerClothes(customerController, hairColor, tshirtColor, trouserColor, skinColor);

        photonView.RPC("NetworkSpawnCustomer", RpcTarget.Others, hairColor, tshirtColor, trouserColor, skinColor, OrderManager.Instance.GenerateOrder(customerController));

    }

    [PunRPC]
    void NetworkSpawnCustomer(int hairColor, int tshirtColor, int trouserColor, int skinColor, int[] orderIDs)
    {
        Debug.Log("Called");
        GameObject customer;
        CustomerController customerController;

        if (inactiveCustomers.Count >= 1)
        {   //GetFromPool
            customerController = inactiveCustomers[0];
            customer = customerController.gameObject;
            inactiveCustomers.Remove(customerController);
            customerController.serviceComplete = false;

            customer.SetActive(true);
            customerController.JoinQueue();
        }
        else
        {   //Instantiate new Customers
            if ((activeCustomers.Count + inactiveCustomers.Count) >= maxCustomers) { return; }
            customer = Instantiate(customerPrefab);
            customerController = customer.GetComponent<CustomerController>();
        }

        customer.SetActive(true);
        customerController.exitWP = exitWP;
        activeCustomers.Add(customerController);

        customer.transform.position = transform.position;

        SetCustomerClothes(customerController, hairColor, tshirtColor, trouserColor, skinColor);
        OrderManager.Instance.GenerateOrder(customerController, orderIDs);
    }

    private void SetCustomerClothes(CustomerController customerController, int hairColor, int tshirtColor, int trouserColor, int skinColor)
    {
        foreach (SkinnedMeshRenderer renderer in customerController.skinnedMeshs)
        {
            renderer.GetPropertyBlock(propBlock);

            switch (renderer.gameObject.name)
            {
                case "HairShort":
                    propBlock.SetColor("_Color", hairColours[hairColor]);
                    break;
                case "TShirt":
                    propBlock.SetColor("_Color", tshirtColours[tshirtColor]);
                    break;
                case "Pants":
                    propBlock.SetColor("_Color", trouserColours[trouserColor]);
                    break;
                case "Male":
                    propBlock.SetColor("_Color", skinColours[skinColor]);
                    break;
                default:
                    break;
            }

            renderer.SetPropertyBlock(propBlock);
        }
    }

    /// <summary>
    /// Move Customer back into Pool
    /// </summary>
    /// <param name="customer"></param>
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
    [ContextMenu("GetDrunkness")]
    public void CallGetDrunkess()
    {
        GetDrunkness();
    }
    public static float GetDrunkness()
    {
        float drunkness = 0;
        foreach (CustomerController c in activeCustomers)
        {
            drunkness += c.drunkness;
        }

        return drunkness / maxCustomers;
    }
}
