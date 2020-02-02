using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject itemList;

    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private GameObject shopPanel;

    [SerializeField]
    private List<ShopItem> items;

    private bool showShop;
    private PlayerController playerController;
    private InputHandler inputHandler;

    void Start()
    {
        showShop = false;

        playerController = FindObjectOfType<PlayerController>();
        inputHandler = FindObjectOfType<InputHandler>();

        BuildList();
    }

    private void BuildList()
    {
        foreach (Transform child in itemList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (ShopItem item in items)
        {
            GameObject itemObj = GameObject.Instantiate(itemPrefab);
            itemObj.transform.SetParent(itemList.transform);
            itemObj.GetComponent<ShopItemUI>().item = item;

            Transform buyButton = itemObj.transform.Find("BuyButton");
            buyButton.GetComponent<Button>().onClick.AddListener(delegate { CheckPrice(item.id, item.price); });

            bool owned = (item.maxNum == item.ownedNum);
            if (owned)
            {
                buyButton.GetComponent<Button>().interactable = false;
            }
            item.go.SetActive(owned);
        }
    }

    public void CheckPrice(string id, int price)
    {
        Debug.Log("I have been clicked " + id);
        if (MoneySystem.BuyItem(price))
        {
            foreach (ShopItem item in items)
            {
                if (item.id == id)
                {
                    item.ownedNum++;
                    break;
                }
            }
            Debug.Log("Item bought!");
            BuildList();
        }
        else
        {
            Debug.Log("Can't afford");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showShop = !showShop;
            if (showShop)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            shopPanel.SetActive(showShop);

            playerController.enabled = !showShop;
            inputHandler.enabled = !showShop;
        }
    }
}
