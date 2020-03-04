using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkSpawner : MonoBehaviour
{

    [System.Serializable]
    public class SpawnPoint
    {
        public Transform transform;
        public GameObject drink;
    }

    [System.Serializable]
    public class DrinkItem
    {
        public string name;
        public string id;
        public GameObject obj;
        public SpawnPoint[] spawnPoints;
        public int itemPos;
        public ShopItemUI shopItem;
    }


    public DrinkItem[] drinks;

    public Shop ui;

    //public static List<int> poolOfIDs = new List<int>{150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187, 188, 189, 190,191,192,193,194,195,196,197,198,199, 200};
    private void Start()
    {
        
        foreach (DrinkItem d in drinks)
        {
            if (ui.items[d.itemPos].ownedNum > 0)
            {
                foreach (SpawnPoint p in d.spawnPoints)
                {
                    if (p.drink == null)
                    {
                        if (ui.items[d.itemPos].ownedNum > 0)
                        {
                            Drink drink = Instantiate(d.obj, p.transform).GetComponent<Drink>(); //PHOTON VIEW 150 - 200 IS RESERVED FOR DRINKS
                            if (PhotonNetwork.IsConnected)
                            {
                                int x = Random.Range(0, 50);
                                drink.GetComponent<PhotonView>().ViewID = 150 + x;
                            }
                            drink.transform.position = p.transform.position;
                            drink.transform.eulerAngles = p.transform.eulerAngles;
                            drink.spawn = p;
                            p.drink = drink.gameObject;
                            ui.items[d.itemPos].ownedNum -= 1;
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            foreach (DrinkItem d in drinks)
            {
                if (ui.items[d.itemPos].ownedNum > 0)
                {
                    foreach (SpawnPoint p in d.spawnPoints)
                    {
                        if (p.drink == null)
                        {
                            if (ui.items[d.itemPos].ownedNum > 0)
                            {
                                Drink drink = Instantiate(d.obj, p.transform).GetComponent<Drink>();
                                drink.transform.position = p.transform.position;
                                drink.transform.eulerAngles = p.transform.eulerAngles;
                                drink.spawn = p;
                                p.drink = drink.gameObject;
                                ui.items[d.itemPos].ownedNum -= 1;
                                drink.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }

}
