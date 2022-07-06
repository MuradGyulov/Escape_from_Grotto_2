using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Frog_AI : MonoBehaviour
{
    [SerializeField] private bool standStill;
    [SerializeField] private bool activePatrol;
    [Space(10)]
    [SerializeField] private int maximumHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float hitEffectDuration;
    [SerializeField] private Color hitEffectColor;
    [SerializeField] private float groundSensorRadius;
    [SerializeField] private float detectionRadius;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(25)]
    [SerializeField] Transform groundSensorPointer;
    [SerializeField] Transform dtectionSensorPointer;
    [Space(8)]
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

    public static UnityEvent monssterIsDead = new UnityEvent();

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
                animator.SetFloat("Frog Move", 0);
            }
            else if (activePatrol)
            {
                rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
                animator.SetFloat("Frog Move", Mathf.Abs(movementSpeed));

                if (!sensorInGround && !targetDetected)
                {
                    SlugFlip();
                    movementSpeed *= -1;
                }
                else if (!sensorInGround && targetDetected)
                {
                    animator.SetFloat("Frog Move", 0);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                standStill = true;
                activePatrol = false;
                break;
            case "Slug":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Dragon":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Box":
                movementSpeed *= -1;
                SlugFlip();
                break;
            case "Frog":
                movementSpeed *= -1;
                SlugFlip();
                break;

            case "Player Bullet":
                maximumHealth--;
                hitEffectMaterial.color = hitEffectColor;
                standStill = false;
                activePatrol = true;
                spriteRenderer.material = hitEffectMaterial;
                StartCoroutine(HitFlashRountime());
                if (maximumHealth <= 0)
                {
                    monssterIsDead.Invoke();
                    animator.SetBool("Frog Dead", true);
                    slugIsDead = true;
                    rigidBody.bodyType = RigidbodyType2D.Static;
                    circleCollider.enabled = false;
                    capsulCollider.enabled = false;
                    Destroy(this.gameObject, 1.8f);
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

    private IEnumerator HitFlashRountime()
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
}