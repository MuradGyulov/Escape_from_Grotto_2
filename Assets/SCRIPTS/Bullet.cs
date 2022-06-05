using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [Space(10)]
    [SerializeField] private ParticleSystem groundImpactParticles;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidbod;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private void OnEnable()
    {
        rigidbod.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;

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
                rigidbod.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                groundImpactParticles.Play();
                break;
            case "Slug":
                gameObject.SetActive(false);
                break;
            case "Box":
                rigidbod.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                groundImpactParticles.Play();
                break;
            case "Stalagmit":
                rigidbod.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                groundImpactParticles.Play();
                break;
            case "Gates":
                rigidbod.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                groundImpactParticles.Play();
                break;
        }
    }
}
