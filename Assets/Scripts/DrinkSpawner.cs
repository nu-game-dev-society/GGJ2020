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
                            Drink drink = Instantiate(d.obj, p.transform).GetComponent<Drink>();
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
        if (Time.frameCount % 60 == 0 )
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
