using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [Space(10)]
    [SerializeField] private int ammountCannonAmmoInPool;
    [SerializeField] private List<GameObject> pooledCannonAmmo;
    [Space(15)]
    [SerializeField] private GameObject cannonAmmoPrefab;

    private float nextShoot;

    private void Start()
    {
        pooledCannonAmmo = new List<GameObject>();

        GameObject ammoInstance;
        for(int i = 0; i < ammountCannonAmmoInPool; i++)
        {
            ammoInstance = Instantiate(cannonAmmoPrefab);
            ammoInstance.SetActive(false);
            pooledCannonAmmo.Add(ammoInstance);
        }

        StartCoroutine(CannonShooting());
    }

    private GameObject GetPooledAmmo()
    {
        for(int i = 0; i < ammountCannonAmmoInPool; i++)
        {
            if (!pooledCannonAmmo[i].activeInHierarchy)
            {
                return pooledCannonAmmo[i];
            }
        }
        return null;
    }

    private IEnumerator CannonShooting()
    {
        while (true)
        {
            GameObject cannonAmmo = GetPooledAmmo();
            if (cannonAmmo != null)
            {
                cannonAmmo.transform.position = transform.position;
                cannonAmmo.transform.rotation = transform.rotation;
                cannonAmmo.SetActive(true);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
