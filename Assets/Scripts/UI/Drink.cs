using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public DrinkSpawner.SpawnPoint spawn;


    public void ClearSpawn()
    {
        if (spawn != null)
        {
            spawn.drink = null;
            spawn = null;
        }
    }
}
