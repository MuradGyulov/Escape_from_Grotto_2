using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Ammo : MonoBehaviour
{
    [SerializeField] public float ammoSpeed;
    [SerializeField] private float ammoLifeTime;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private ParticleSystem collisionEffect;
    [SerializeField] private ParticleSystem trailEffect;

    private bool ammoIsExpoloded = false;

    private void OnEnable()
    {
        ammoIsExpoloded = false;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
        StartCoroutine(EndAmmoLifetime());

        rigidBody2D.AddForce(transform.right * ammoSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                ammoIsExpoloded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                StartCoroutine(EnableAmmoBeforeCollision());
                break;
            case "Ground":
                ammoIsExpoloded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                StartCoroutine(EnableAmmoBeforeCollision());
                break;
            case "Cannon Ammo":
                ammoIsExpoloded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                StartCoroutine(EnableAmmoBeforeCollision());
                break;
            case "Box":
                ammoIsExpoloded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                StartCoroutine(EnableAmmoBeforeCollision());
                break;
            case "Stalagmit":
                ammoIsExpoloded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                StartCoroutine(EnableAmmoBeforeCollision());
                break;
        }
    }

    private IEnumerator EnableAmmoBeforeCollision()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);       
    }

    private IEnumerator EndAmmoLifetime()
    {
        yield return new WaitForSeconds(ammoLifeTime);
        if (!ammoIsExpoloded)
        {
            ammoIsExpoloded = true;
            rigidBody2D.bodyType = RigidbodyType2D.Static;
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            trailEffect.Stop();
            collisionEffect.Play();
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
