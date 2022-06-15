using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_AI : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private float targetDetectionRadius;
    [SerializeField] private float targetPursitRadius;
    [Space(15)]
    [SerializeField] private Transform target;
    [Space(28)]
    [SerializeField] private LayerMask whoIsPlayer;
    [Space(10)]
    [SerializeField] private Transform targetDetectionSensorPointer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private CircleCollider2D circleCollider;



    private bool facingRight = true;
    private bool targetCaptured;
    private bool playerIsDeadOrWin = false;

    private void Start()
    {
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


            if (targetCaptured)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                flySpeed = 0;
                rigidBody2D.velocity = Vector2.zero;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                break;

            case "Bullet":
                maxHealth--;
                if(maxHealth <= 0)
                {
                    animator.SetBool("Eye Dead", true);
                    rigidBody2D.gravityScale = 1;
                    circleCollider.isTrigger = false;
                    Destroy(this.gameObject, 2f);
                }
                break;
        }
    }
}


