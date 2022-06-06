using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [Space(10)]
    [SerializeField] private ParticleSystem grounHitParticles;
    [SerializeField] private ParticleSystem slugHitParticles;
    [SerializeField] private ParticleSystem boxHitParticles;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidbod;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;


    private void OnEnable()
    {
        rigidbod.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;

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
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                grounHitParticles.Play();
                break;
            case "Slug":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                slugHitParticles.Play();
                break;
            case "Box":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                boxHitParticles.Play();
                break;
            case "Stalagmit":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                grounHitParticles.Play();
                break;
            case "Key Block":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                grounHitParticles.Play();
                break;
        }
    }
}
