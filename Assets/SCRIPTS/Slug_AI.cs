using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Slug_AI : MonoBehaviour
{
    [SerializeField] private bool standStill;
    [SerializeField] private bool activePatrol;
    [Space(10)]
    [SerializeField] private int maximumHealth;
    [SerializeField] private float movementSpeed;
    [Space(10)]
    [SerializeField] private Color hitEffectColor;
    [SerializeField] private float hitEffectDuration;
    [Space(10)]
    [SerializeField] private float groundSensorRadius;
    [SerializeField] private float detectionRadius;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(60)]
    [SerializeField] Transform groundSensorPointer;
    [SerializeField] Transform dtectionSensorPointer;
    [Space(10)]
    [SerializeField] private Material hitEffectMaterial;
    [SerializeField] private CapsuleCollider2D capsulCollider;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private Material originalMaterial;

    private Transform target;

    private bool facingRight = true;
    private bool sensorInGround = true;
    private bool targetDetected = false;
    private bool slugIsDead = false;

    public static UnityEvent monsterIsDead = new UnityEvent();

    private void Start()
    {
        originalMaterial = spriteRenderer.material;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        if (!slugIsDead)
        {
            sensorInGround = Physics2D.OverlapCircle(groundSensorPointer.position, groundSensorRadius, whatIsGround);
            targetDetected = Physics2D.OverlapCircle(dtectionSensorPointer.position, detectionRadius, whoIsPlayer);

            if (standStill)
            {
                rigidBody.velocity = Vector2.zero;
                animator.SetFloat("Slug Move", 0);
            }
            else if (activePatrol)
            {
                rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
                animator.SetFloat("Slug Move", Mathf.Abs(movementSpeed));

                if (!sensorInGround && !targetDetected)
                {
                    SlugFlip();
                    movementSpeed *= -1;
                }
                else if (!sensorInGround && targetDetected)
                {
                    animator.SetFloat("Slug Move", 0);
                    rigidBody.velocity = Vector2.zero;
                }
                else if (targetDetected)
                {
                    if (target.position.x < transform.position.x && facingRight)
                    {
                        SlugFlip();
                        movementSpeed *= -1;
                    }
                    else if (target.position.x > transform.position.x && !facingRight)
                    {
                        SlugFlip();
                        movementSpeed *= -1;
                    }
                }
            }
        }
    }

    private void SlugFlip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator ReturnOriginalMaterial()
    {
        yield return new WaitForSeconds(hitEffectDuration);
        spriteRenderer.material = originalMaterial;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundSensorPointer.position, groundSensorRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dtectionSensorPointer.position, detectionRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                standStill = true;
                activePatrol = false;
                break;
            case "Box":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Slug":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Frog":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Dragon":
                movementSpeed *= -1;
                SlugFlip();
                break;

            case "Player Bullet":
                maximumHealth--;
                hitEffectMaterial.color = hitEffectColor;
                standStill = false;
                activePatrol = true;
                spriteRenderer.material = hitEffectMaterial;
                StartCoroutine(ReturnOriginalMaterial());
                if(maximumHealth <= 0)
                {
                    monsterIsDead.Invoke();
                    animator.SetBool("Slug Dead", true);
                    slugIsDead = true;
                    rigidBody.bodyType = RigidbodyType2D.Static;
                    circleCollider.enabled = false;
                    capsulCollider.enabled = false;
                    Destroy(this.gameObject, 2f);
                }

                if (target.position.x < transform.position.x && facingRight)
                {
                    SlugFlip();
                    movementSpeed *= -1;
                }
                else if (target.position.x > transform.position.x && !facingRight)
                {
                    SlugFlip();
                    movementSpeed *= -1;
                }
                break;
        }
    }
}
