using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Pools : MonoBehaviour
{
    [SerializeField] int ammountInBulletsPool;
    [SerializeField] private List<GameObject> pooledBullets;
    [Space(10)]
    [SerializeField] int ammountInSleeversPool;
    [SerializeField] private List<GameObject> pooledSleevers;
    [Space(80)]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject sleevePrefab;


    private void Start()
    {
        pooledBullets = new List<GameObject>();
        pooledSleevers = new List<GameObject>();

        GameObject bulletInstace;
        for (int i = 0; i < ammountInBulletsPool; i++)
        {
            bulletInstace = Instantiate(bulletPrefab);
            bulletInstace.SetActive(false);
            pooledBullets.Add(bulletInstace);
        }

        GameObject sleeveInstance;
        for(int i = 0; i < ammountInSleeversPool; i++)
        {
            sleeveInstance = Instantiate(sleevePrefab);
            sleeveInstance.SetActive(false);
            pooledSleevers.Add(sleeveInstance);
        }
    }

    public GameObject GetPooledBullet()
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

    public GameObject GetPooledSleeve()
    {
        for (int i = 0; i < ammountInSleeversPool; i++)
        {
            if (!pooledSleevers[i].activeInHierarchy)
            {
                return pooledSleevers[i];
            }
        }
        return null;
    }
}
