using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Fireball : MonoBehaviour
{
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float fireBallLifeTime;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private ParticleSystem particSystem;

    private void OnEnable()
    {
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;


        rigidBody2D.AddForce(transform.right * fireBallSpeed, ForceMode2D.Impulse);
        StartCoroutine(DisableFireBall());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;
                particSystem.Play();
                break;
            case "Player":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;
                particSystem.Play();
                break;
            case "Box":
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;
                particSystem.Play();
                break;
        }
    }

    private IEnumerator DisableFireBall()
    {
        yield return new WaitForSeconds(fireBallLifeTime);
        gameObject.SetActive(false);
    }
}
