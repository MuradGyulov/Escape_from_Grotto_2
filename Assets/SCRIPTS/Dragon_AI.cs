using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dragon_AI : MonoBehaviour
{
    [SerializeField] private bool standStill;
    [SerializeField] private bool defence;
    [SerializeField] private bool activePatrol;
    [Space(10)]
    [SerializeField] private int maximumHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private Vector2 attacRadiusSizes;
    [SerializeField] private float groundSensorRadius;
    [SerializeField] private float detectionSensorRadius;
    [Space(10)]
    [SerializeField] private float hitEffectDuration;
    [SerializeField] private Color hitEffectColor;

    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(25)]
    [SerializeField] Transform groundSensorPointer;
    [SerializeField] Transform detectionSensorPointer;
    [SerializeField] Transform attackRadiusPointer;
    [SerializeField] Transform fireballsLouncherPointer;
    [Space(8)]
    [SerializeField] private Material hitEffectMaterial;
    [SerializeField] private CapsuleCollider2D capsulCollider;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [Space(25)]
    [SerializeField] int ammountInFireballsInPool;
    [SerializeField] private List<GameObject> pooledFireballs;
    [Space(10)]
    [SerializeField] private GameObject fireballPrefab;

    private Material originalMaterial;

    private Transform target;

    private bool facingRight = true;
    private bool sensorInGround = true;
    private bool targetDetected = false;
    private bool dragonIsDead = false;
    private bool canAttack = false;
    private float nextShoot;

    public static UnityEvent monsterIsDead = new UnityEvent();

    private void Start()
    {
        originalMaterial = spriteRenderer.material;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();


        pooledFireballs = new List<GameObject>();
        GameObject fireBallInstance;

        for (int i = 0; i < ammountInFireballsInPool; i++)
        {
            fireBallInstance = Instantiate(fireballPrefab);
            fireBallInstance.SetActive(false);
            pooledFireballs.Add(fireBallInstance);
        }
    }

    private GameObject GetPooledFireBall()
    {
        for(int i = 0; i < ammountInFireballsInPool; i++)
        {
            if (!pooledFireballs[i].activeInHierarchy)
            {
                return pooledFireballs[i];
            }
        }
        return null;
    }


    private void FixedUpdate()
    {
        if (!dragonIsDead)
        {
            sensorInGround = Physics2D.OverlapCircle(groundSensorPointer.position, groundSensorRadius, whatIsGround);
            targetDetected = Physics2D.OverlapCircle(detectionSensorPointer.position, detectionSensorRadius, whoIsPlayer);
            canAttack = Physics2D.OverlapBox(attackRadiusPointer.position, attacRadiusSizes, 0, whoIsPlayer);

            if (standStill)
            {
                rigidBody.velocity = Vector2.zero;
            }
            else if (defence)
            {
                rigidBody.velocity = Vector2.zero;

                if (targetDetected)
                {
                    if (target.position.x < transform.position.x && facingRight)
                    {
                        DragonFlip();
                        movementSpeed *= -1;
                    }
                    else if (target.position.x > transform.position.x && !facingRight)
                    {
                        DragonFlip();
                        movementSpeed *= -1;
                    }
                }

                if (canAttack)
                {
                    animator.SetBool("Dragon Attack", true);
                    if (Time.time > nextShoot)
                    {
                        nextShoot = Time.time + fireRate;
                        GameObject fireBall = GetPooledFireBall();
                        if (fireBall != null)
                        {
                            fireBall.transform.position = fireballsLouncherPointer.position;
                            fireBall.transform.rotation = fireballsLouncherPointer.rotation;
                            fireBall.SetActive(true);
                        }
                    }
                }
                else
                {
                    animator.SetBool("Dragon Attack", false);
                }
            }
            else if (activePatrol)
            {
                rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
                animator.SetFloat("Dragon Move", Mathf.Abs(movementSpeed));

                if (!sensorInGround && !targetDetected)
                {
                    DragonFlip();
                    movementSpeed *= -1;
                }
                else if (!sensorInGround && targetDetected)
                {
                    animator.SetFloat("Dragon Move", 0);
                    rigidBody.velocity = Vector2.zero;
                }
                else if (targetDetected)
                {
                    if (target.position.x < transform.position.x && facingRight)
                    {
                        DragonFlip();
                        movementSpeed *= -1;
                    }
                    else if (target.position.x > transform.position.x && !facingRight)
                    {
                        DragonFlip();
                        movementSpeed *= -1;
                    }
                }
                else if (canAttack)
                {
                    rigidBody.velocity = Vector2.zero;
                    animator.SetBool("Dragon Attack", true);

                    if(Time.time > nextShoot)
                    {
                        nextShoot = Time.time + fireRate;
                        GameObject fireBall = GetPooledFireBall();
                        if (fireBall != null)
                        {
                            fireBall.transform.position = fireballsLouncherPointer.position;
                            fireBall.transform.rotation = fireballsLouncherPointer.rotation;
                            fireBall.SetActive(true);
                        }
                    }
                }
                else
                {
                    animator.SetBool("Dragon Attack", false);
                }
            }
        }
    }

    private void DragonFlip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        if (transform.localScale.x == 1)
        {
            fireballsLouncherPointer.eulerAngles = new Vector3(0, 0, 0);
            fireballsLouncherPointer.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.localScale.x == -1)
        {
            fireballsLouncherPointer.eulerAngles = new Vector3(0, 180, 0);
            fireballsLouncherPointer.eulerAngles = new Vector3(0, 180, 0);
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionSensorPointer.position, detectionSensorRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackRadiusPointer.position, attacRadiusSizes);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                standStill = true;
                activePatrol = false;
                defence = false;
                animator.SetBool("Dragon Attack", false);
                animator.SetFloat("Dragon Move", 0);
                break;
            case "Box":
                movementSpeed *= -1;
                DragonFlip();
                break;
            case "Slug":
                movementSpeed *= -1;
                DragonFlip();
                break;
            case "Dragon":
                movementSpeed *= -1;
                DragonFlip();
                break;
            case "Skeleton":
                movementSpeed *= -1;
                DragonFlip();
                break;
            case "Frog":
                movementSpeed *= -1;
                DragonFlip();
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
                    monsterIsDead.Invoke();
                    animator.SetBool("Dragon Attack", false);
                    animator.SetBool("Dragon Dead", true);
                    dragonIsDead = true;
                    rigidBody.bodyType = RigidbodyType2D.Static;
                    circleCollider.enabled = false;
                    capsulCollider.enabled = false;
                    Destroy(this.gameObject, 2);
                }

                if (target.position.x < transform.position.x && facingRight)
                {
                    DragonFlip();
                    movementSpeed *= -1;
                }
                else if (target.position.x > transform.position.x && !facingRight)
                {
                    DragonFlip();
                    movementSpeed *= -1;
                }
                break;
        }
    }
}
