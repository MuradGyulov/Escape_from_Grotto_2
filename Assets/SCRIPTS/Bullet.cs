using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidbod;


    private void OnEnable()
    {
        rigidbod.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(BulletDisable());
    }

    private IEnumerator BulletDisable()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                gameObject.SetActive(false);
                break;
            case "Slug":
                gameObject.SetActive(false);
                break;
            case "Box":
                gameObject.SetActive(false);
                break;
            case "Stalagmit":
                gameObject.SetActive(false);
                break;
            case "Gates":
                gameObject.SetActive(false);
                break;
        }
    }
}
