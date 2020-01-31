using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    //instance of MoneySystem
    private static MoneySystem mInstance;

    //Current balance
    public int mMoney = 0;

    //interval for saving money to prefrences
    public float saveInterval = 3;

    private  static MoneySystem instance
    {
        get
        {
            if (mInstance == null)
            {
                if (GameObject.Find("MoneySystem"))
                {
                    GameObject gameObject = GameObject.Find("MoneySystem");
                    if (gameObject.GetComponent<MoneySystem>())
                    {
                        mInstance = gameObject.GetComponent<MoneySystem>();
                    }
                    else
                    {
                        mInstance = gameObject.AddComponent<MoneySystem>();
                    }
                }
                else
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = "MoneySystem";
                    mInstance = gameObject.AddComponent<MoneySystem>();
                }
            }
            return mInstance;
        }

        set
        {
            mInstance = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "MoneySystem";
        mInstance = this;

        //load saved money
        AddMoney(PlayerPrefs.GetInt("MoneySave", 0));

        //start the save interval
        StartCoroutine("SaveMoney");
    }

    //while reality exists save money each interval
    public IEnumerator saveMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(saveInterval);
            PlayerPrefs.SetInt("MoneySave", mInstance.mMoney);
        }
    }

    //check if you have enough money to buy 
    public static  bool BuyItem(int cost)
    {
        if (mInstance.mMoney - cost >= 0)
        {
            mInstance.mMoney -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get current balance
    public static int GetMoney()
    {
        return mInstance.mMoney;
    }


    //Add money to balance
    public static void AddMoney(int amount)
    {
        mInstance.mMoney += amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
