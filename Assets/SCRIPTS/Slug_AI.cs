using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug_AI : MonoBehaviour
{
    [SerializeField] private bool standStill;
    [SerializeField] private bool passivePatrol;
    [SerializeField] private bool activePatrol;
    [Space(10)]
    [SerializeField] Transform target;
    [Space(10)]
    [SerializeField] private int maximumHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float hitEffectDuration;
    [SerializeField] private Color hitEffectColor;
    [SerializeField] private float groundSensorRadius;
    [SerializeField] private float chaseSensorRadius;
    [SerializeField] private float attackSensorRadius;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(25)]
    [SerializeField] Transform groundSensorPointer;
    [SerializeField] Transform attackSensorsPointer;
    [Space(8)]
    [SerializeField] private Material hitEffectMaterial;
    [SerializeField] private CapsuleCollider2D capsulCollider;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private Material originalMaterial;

    private bool facingRight = true;
    private bool sensorInGround = true;
    private bool targetCaptured = false;
    private bool mayChase = false;
    private bool slugIsDead = false;

    private void Start()
    {
        originalMaterial = spriteRenderer.material;
    }


    private void FixedUpdate()
    {
        if (!slugIsDead)
        {
            sensorInGround = Physics2D.OverlapCircle(groundSensorPointer.position, groundSensorRadius, whatIsGround);
            targetCaptured = Physics2D.OverlapCircle(attackSensorsPointer.position, attackSensorRadius, whoIsPlayer);
            mayChase = Physics2D.OverlapCircle(attackSensorsPointer.position, chaseSensorRadius, whoIsPlayer);

            if (standStill)
            {
                rigidBody.velocity = Vector2.zero;
                animator.SetFloat("Move", 0);
            }
            else if (passivePatrol)
            {
                rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
                animator.SetFloat("Move", Mathf.Abs(movementSpeed));

                if (!sensorInGround)
                {
                    SlugFlip();
                    movementSpeed *= -1;
                }
            }
            else if (activePatrol)
            {
                rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
                animator.SetFloat("Move", Mathf.Abs(movementSpeed));

                if (!sensorInGround && !mayChase)
                {
                    SlugFlip();
                    movementSpeed *= -1;
                }
                else if (!sensorInGround && mayChase)
                {
                    animator.SetFloat("Move", 0);
                    rigidBody.velocity = Vector2.zero;
                }

                if (targetCaptured)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundSensorPointer.position, groundSensorRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackSensorsPointer.position, attackSensorRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackSensorsPointer.position, chaseSensorRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                standStill = true;
                passivePatrol = false;
                activePatrol = false;
                break;
            case "Slug":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Box":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Bullet":
                maximumHealth--;
                hitEffectMaterial.color = hitEffectColor;
                standStill = false;
                activePatrol = true;
                spriteRenderer.material = hitEffectMaterial;
                StartCoroutine(HitFlashRountime());
                if(maximumHealth <= 0)
                {
                    animator.SetBool("Dead", true);
                    slugIsDead = true;
                    rigidBody.bodyType = RigidbodyType2D.Static;
                    circleCollider.enabled = false;
                    capsulCollider.enabled = false;
                    Destroy(this.gameObject, 1);
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
            case "Stalagmit":
                maximumHealth--;
                hitEffectMaterial.color = hitEffectColor;
                standStill = false;
                activePatrol = true;
                spriteRenderer.material = hitEffectMaterial;
                StartCoroutine(HitFlashRountime());
                if (maximumHealth <= 0)
                {
                    animator.SetBool("Dead", true);
                    slugIsDead = true;
                    rigidBody.bodyType = RigidbodyType2D.Static;
                    circleCollider.enabled = false;
                    capsulCollider.enabled = false;
                    Destroy(this.gameObject, 1);
                }
                break;
        }
    }

    private IEnumerator HitFlashRountime()
    {
        yield return new WaitForSeconds(hitEffectDuration);
        spriteRenderer.material = originalMaterial;
    }
}
