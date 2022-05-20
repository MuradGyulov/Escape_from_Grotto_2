using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidbod;


    private void OnEnable()
    {
        rigidbod.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        Invoke("BulletDeactivate", bulletLifeTime);
    }

    private void BulletDeactivate()
    {
        gameObject.SetActive(false);
    }

    //private void OnDestroy()
    //{

    //}
}
