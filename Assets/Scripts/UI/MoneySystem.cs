using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    //instance of MoneySystem
    private static MoneySystem mInstance;

    //Current balance
    public int mMoney = 100;

    //interval for saving money to prefrences
    public float saveInterval = 3;

    private static MoneySystem instance
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
        //AddMoney(PlayerPrefs.GetInt("MoneySave", 0));

        //start the save interval
        //StartCoroutine(SaveMoney());
    }

    //while reality exists save money each interval
    public IEnumerator SaveMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(saveInterval);
            PlayerPrefs.SetInt("MoneySave", instance.mMoney);
        }
    }

    //check if you have enough money to buy 
    public static bool BuyItem(int cost)
    {
        if (instance.mMoney - cost >= 0)
        {
            instance.mMoney -= cost;
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
        return instance.mMoney;
    }

    //Add money to balance
    public static void AddMoney(int amount)
    {
        instance.mMoney += amount;
    }


    //Take money to balance
    public static void TakeMoney(int amount)
    {
        instance.mMoney -= amount;
    }

    public static string FormatMoney(int amount)
    {
        return "£" + String.Format("{0:n0}", amount);
    }
}
