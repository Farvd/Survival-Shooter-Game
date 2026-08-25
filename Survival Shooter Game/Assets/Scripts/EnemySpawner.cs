using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;





public class EnemySpawner : MonoBehaviour
{

    


    public bool Spawnable = true;
    public Transform SpawnLocation;
    public GameObject EnemyPrefab;

    void OnCollisionStay(Collision collision)
    {
        Spawnable = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        Spawnable = true;
    }


    void Start()
    {
        SpawnEnemy(100);
    }


    public void SpawnEnemy(int amount)
    {
        if (EnemyPrefab == null)
        {
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-200, 200), 0, Random.Range(-200, 200));
            if (!Spawnable) continue;

            GameObject newEnemy = Instantiate(EnemyPrefab, spawnPos, SpawnLocation != null ? SpawnLocation.rotation : Quaternion.identity);



        }
    }
}
