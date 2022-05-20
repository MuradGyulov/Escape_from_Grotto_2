using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Pools : MonoBehaviour
{
    [SerializeField] int ammountInBulletsPool;
    [SerializeField] private List<GameObject> pooledBullets;
    [Space(80)]
    [SerializeField] private GameObject bulletPrefab;


    private void Start()
    {
        pooledBullets = new List<GameObject>();

        GameObject bulletInstace;
        for (int i = 0; i < ammountInBulletsPool; i++)
        {
            bulletInstace = Instantiate(bulletPrefab);
            bulletInstace.SetActive(false);
            pooledBullets.Add(bulletInstace);
        }
    }

    public GameObject GetPolledBullet()
    {
        for(int i = 0; i < ammountInBulletsPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
