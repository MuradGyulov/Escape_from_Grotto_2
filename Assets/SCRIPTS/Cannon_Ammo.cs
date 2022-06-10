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

    private void OnEnable()
    {
        rigidBody2D.AddForce(transform.right * ammoSpeed, ForceMode2D.Impulse);
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
        Invoke("EnableAmmo", ammoLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                Invoke("EnableAmmo", 1);
                break;
            case "Ground":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                Invoke("EnableAmmo", 1);
                break;
            case "Box":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                Invoke("EnableAmmo", 1);
                break;
            case "Stalagmit":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                trailEffect.Stop();
                collisionEffect.Play();
                Invoke("EnableAmmo", 1);
                break;
        }
    }

    private void EnableAmmo()
    {
        gameObject.SetActive(false);
    }
}
