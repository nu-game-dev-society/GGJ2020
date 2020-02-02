using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public ShopItem item;

    [SerializeField]
    private TextMeshProUGUI ProductName;
    [SerializeField]
    private TextMeshProUGUI ProductDesc;
    [SerializeField]
    private GameObject BuyButton;
    [SerializeField]
    private Image ProductImage;

    // Start is called before the first frame update
    void Start()
    {
        ProductName.text = item.name;
        ProductDesc.text = item.desc;
        BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = MoneySystem.FormatMoney(item.price);

        if (item.image)
        {
            ProductImage.sprite = item.image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
