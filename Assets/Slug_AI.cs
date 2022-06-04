using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug_AI : MonoBehaviour
{
    //[Tooltip("Hello There!")]
    [SerializeField] private bool idle;
    [SerializeField] private bool pasivePatrol;
    [SerializeField] private bool activePatrol;
    [Space(10)]
    [SerializeField] private int health;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float movingSpeedInAngryState;
    [SerializeField] private float angryStateDuration;
    [SerializeField] private float hitFlashDuriation;
    [SerializeField] private Color hitFlashColor;
    [Space(10)]
    [SerializeField] private float groundSensorRadius;
    [SerializeField] private float chaseSensorRadius;
    [SerializeField] private float attackSensorRadius;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(25)]
    [SerializeField] Transform groundSensor;
    [SerializeField] Transform attackSensorsPointer;
    [SerializeField] Transform target;
    [Space(8)]
    [SerializeField] private Material hitFlashMateria;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator slugAnimator;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private CapsuleCollider2D capsulCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private bool facingRight = true;
    private bool sensorOnGround = true;
    private bool targetCapture = false;
    private bool canChase = false;
    private bool slugIsDead = false;
    private bool angryStateBeforeHit = false;

    private float originalSpeed;

    private void Start()
    {
        originalMaterial = spriteRenderer.material;
        originalSpeed = movingSpeed;
    }


    private void FixedUpdate()
    {
        if (!slugIsDead)
        {
            sensorOnGround = Physics2D.OverlapCircle(groundSensor.position, groundSensorRadius, whatIsGround);
            targetCapture = Physics2D.OverlapCircle(attackSensorsPointer.position, attackSensorRadius, whoIsPlayer);
            canChase = Physics2D.OverlapCircle(attackSensorsPointer.position, chaseSensorRadius, whoIsPlayer);

            if (idle)
            {
                rigidBody.velocity = Vector2.zero;
                slugAnimator.SetFloat("Move", 0);
            }
            else if (pasivePatrol)
            {
                rigidBody.velocity = new Vector2(movingSpeed, rigidBody.velocity.y);
                slugAnimator.SetFloat("Move", Mathf.Abs(movingSpeed));

                if (!sensorOnGround)
                {
                    SlugFlip();
                    movingSpeed *= -1;
                }
            }
            else if (activePatrol)
            {
                rigidBody.velocity = new Vector2(movingSpeed, rigidBody.velocity.y);
                slugAnimator.SetFloat("Move", Mathf.Abs(movingSpeed));

                if (!sensorOnGround && !canChase)
                {
                    SlugFlip();
                    movingSpeed *= -1;
                }
                else if (!sensorOnGround && canChase)
                {
                    slugAnimator.SetFloat("Move", 0);
                    rigidBody.velocity = Vector2.zero;
                }

                if (targetCapture)
                {
                    if (target.position.x < transform.position.x && facingRight)
                    {
                        SlugFlip();
                        movingSpeed *= -1;
                    }
                    else if (target.position.x > transform.position.x && !facingRight)
                    {
                        SlugFlip();
                        movingSpeed *= -1;
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
        Gizmos.DrawWireSphere(groundSensor.position, groundSensorRadius);

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
                idle = true;
                pasivePatrol = false;
                activePatrol = false;
                angryStateBeforeHit = false; // There !!!!!!!!!!!!!!!!!!!!!!
                break;
            case "Slug":
                movingSpeed *= -1;
                SlugFlip();
                break;
            case "Bullet":
                health--;
                angryStateBeforeHit = true;
                //movingSpeed = movingSpeedInAngryState;
                StartCoroutine(AngryStateTimer());
                hitFlashMateria.color = hitFlashColor;
                spriteRenderer.material = hitFlashMateria;
                StartCoroutine(HitFlashRountime());
                if(health <= 0)
                {
                    slugAnimator.SetBool("Dead", true);
                    slugIsDead = true;
                    rigidBody.bodyType = RigidbodyType2D.Static;
                    circleCollider.enabled = false;
                    capsulCollider.enabled = false;
                    Destroy(this.gameObject, 1);
                }

                if (target.position.x < transform.position.x && facingRight)
                {
                    SlugFlip();
                    movingSpeed *= -1;
                }
                else if (target.position.x > transform.position.x && !facingRight)
                {
                    SlugFlip();
                    movingSpeed *= -1;
                }
                break;
        }
    }

    private IEnumerator HitFlashRountime()
    {
        yield return new WaitForSeconds(hitFlashDuriation);
        spriteRenderer.material = originalMaterial;
    }

    private IEnumerator AngryStateTimer()
    {
        yield return new WaitForSeconds(angryStateDuration);
        angryStateBeforeHit = false;
        //movingSpeed = originalSpeed;
    }
}
