using System.Collections;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [Space(30)]
    [SerializeField] private ParticleSystem grounHitParticles;
    [SerializeField] private ParticleSystem boxHitParticles;
    [Space(10)]
    [SerializeField] private ParticleSystem slugHitParticles;
    [SerializeField] private ParticleSystem dragonHitParticles;
    [SerializeField] private ParticleSystem frogHitParticles;
    [SerializeField] private ParticleSystem eyeHitParticles;
    [SerializeField] private ParticleSystem sceletonHitParticles;
    [Space(25)]
    [SerializeField] private Rigidbody2D rigidbod;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;


    private void OnEnable()
    {
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
        rigidbod.bodyType = RigidbodyType2D.Dynamic;
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
            case "Box":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                boxHitParticles.Play();
                break;
            case "Dangerious":
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
            case "Frog":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                frogHitParticles.Play();
                break;
            case "Dragon":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                dragonHitParticles.Play();
                break;
            case "Eye":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                eyeHitParticles.Play();
                break;
            case "Skeleton":
                rigidbod.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                spriteRenderer.enabled = false;
                sceletonHitParticles.Play();
                break;
        }        
    }
}
