using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Eye_AI : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private float targetDetectionRadius;
    [SerializeField] private float targetPursitRadius;
    [SerializeField] private float hitEffectDuriation;
    [SerializeField] private Color hitEffectColor;
    [Space(28)]
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(10)]
    [SerializeField] private Material hitEffectMaterial;
    [SerializeField] private Transform targetDetectionSensorPointer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D circleCollider;


    private Material originalMaterial;

    private Transform target;

    private bool facingRight = true;
    private bool targetCaptured;
    private bool playerIsDeadOrWin = false;
    private bool targerShootMe = false;

    public static UnityEvent monsterIsDead = new UnityEvent();

    private void Start()
    {
        originalMaterial = spriteRenderer.material;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();

        Player_Actions.PlayerIsWin.AddListener(PlayerIsDeadOrWin);
        Player_Actions.PlayerIsDead.AddListener(PlayerIsDeadOrWin);
    }

    private void PlayerIsDeadOrWin()
    {
        playerIsDeadOrWin = true;
    }


    private void FixedUpdate()
    {
        if (!playerIsDeadOrWin)
        {
            if (!targetCaptured)
            {
                targetCaptured = Physics2D.OverlapCircle(targetDetectionSensorPointer.position, targetDetectionRadius, whoIsPlayer);
            }
            else if (targetCaptured = Physics2D.OverlapCircle(targetDetectionSensorPointer.position, targetPursitRadius, whoIsPlayer))
            {
                targetCaptured = true;
            }
            else
            {
                targetCaptured = false;
            }


            if (targetCaptured || targerShootMe)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, flySpeed);

                if (target.position.x < transform.position.x && facingRight)
                {
                    SlugFlip();
                }
                else if (target.position.x > transform.position.x && !facingRight)
                {
                    SlugFlip();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetDetectionSensorPointer.position, targetDetectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetDetectionSensorPointer.position, targetPursitRadius);
    }

    private IEnumerator ReturnDefaultColor()
    {
        yield return new WaitForSeconds(hitEffectDuriation);
        spriteRenderer.material = originalMaterial;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                playerIsDeadOrWin = true;
                break;

            case "Player Bullet":
                if (!playerIsDeadOrWin)
                {
                    hitEffectMaterial.color = hitEffectColor;
                    spriteRenderer.material = hitEffectMaterial;
                    StartCoroutine(ReturnDefaultColor());
                    targerShootMe = true;
                    maxHealth--;
                    if (maxHealth <= 0)
                    {
                        monsterIsDead.Invoke();
                        playerIsDeadOrWin = true;
                        gameObject.tag = "Untagged";
                        circleCollider.isTrigger = false;
                        animator.SetBool("Eye Dead", true);
                        rigidBody2D.gravityScale = 1;
                        Destroy(this.gameObject, 1f);
                    }
                }
                break;
        }
    }
}


