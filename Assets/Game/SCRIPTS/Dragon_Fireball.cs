using System.Collections;
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

    private bool fireballIsExploded = false;

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidBody2D.AddForce(transform.right * fireBallSpeed, ForceMode2D.Impulse);
        StartCoroutine(EndFireballLifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                fireballIsExploded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;
                particSystem.Play();
                StartCoroutine(DisableFireballBeforeCollision());
                break;
            case "Player":
                fireballIsExploded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;
                particSystem.Play();
                StartCoroutine(DisableFireballBeforeCollision());
                break;
            case "Box":
                fireballIsExploded = true;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;
                particSystem.Play();
                StartCoroutine(DisableFireballBeforeCollision());
                break;
        }
    }

    private IEnumerator DisableFireballBeforeCollision()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private IEnumerator EndFireballLifeTime()
    {
        yield return new WaitForSeconds(fireBallLifeTime);
        if (!fireballIsExploded)
        {
            rigidBody2D.bodyType = RigidbodyType2D.Static;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            particSystem.Play();
        }

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
