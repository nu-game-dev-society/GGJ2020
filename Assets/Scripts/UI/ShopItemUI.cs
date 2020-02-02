using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public ShopItem item;

    [SerializeField]
    private TextMeshProUGUI ProductName;
    [SerializeField]
    private TextMeshProUGUI ProductDesc;
    [SerializeField]
    private GameObject BuyButton;

    // Start is called before the first frame update
    void Start()
    {
        ProductName.text = item.name;
        ProductDesc.text = item.desc;
        BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = MoneySystem.FormatMoney(item.price);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
