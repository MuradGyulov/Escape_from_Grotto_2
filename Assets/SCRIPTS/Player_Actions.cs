using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actions : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    [Space(10)]
    [SerializeField] private float touchDownSensorRadius;
    [SerializeField] private float boxTouchSensorRadius;
    [Space(8)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsBox;
    [Space(60)]
    [SerializeField] private Transform touchDownSensor;
    [SerializeField] private Transform touchBoxSensor;
    [SerializeField] private Transform bulletLouncher;
    [Space(10)]
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private Collider2D boxCollid2D;
    [SerializeField] private Animator animator;

    private Player_Pools playerPools;

    private float nextShoot;
    private bool facingRight;
    private bool isGrounded;
    private bool isTouchBox;


    private void Start()
    {
        Player_Input.IsJump.AddListener(PlayerJump);

        playerPools = GetComponent<Player_Pools>();
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        animator.SetBool("Jump", true);
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(touchDownSensor.position, touchDownSensorRadius, whatIsGround);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            switch (groundColliders[i].gameObject.layer)
            {
                case 11:
                    isGrounded = true;
                    animator.SetBool("Jump", false);
                    break;
                case 12:
                    isGrounded = true;
                    animator.SetBool("Jump", false);
                    break;
            }
        }

        isTouchBox = false;
        Collider2D[] boxCollider = Physics2D.OverlapCircleAll(touchBoxSensor.position, boxTouchSensorRadius, whatIsBox);
        for (int i = 0; i < boxCollider.Length; i++)
        {
            if(boxCollider[i].gameObject.layer == 12)
            {
                isTouchBox = true;
            }
        }

        PlayerRun();
        PlayerPush();
        PlayerShoot();
    }

    private void PlayerRun()
    {
        rigid2D.velocity = new Vector2(Player_Input.Horizontal * runSpeed, rigid2D.velocity.y);
        animator.SetFloat("Run", Mathf.Abs(Player_Input.Horizontal));

        if (Player_Input.Horizontal > 0 && facingRight) { PlayerFlip(); }
        else if (Player_Input.Horizontal < 0 && !facingRight) { PlayerFlip(); }
    }

    private void PlayerPush()
    {
        if (isGrounded && isTouchBox)
        {
            rigid2D.velocity = new Vector2(Player_Input.Horizontal, rigid2D.velocity.y);
            animator.SetBool("Push", true);
            boxCollid2D.enabled = true;
        }
        else
        {
            animator.SetBool("Push", false);
            boxCollid2D.enabled = false;
        }
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            rigid2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
        }
    }

    private void PlayerShoot()
    {
        if (isGrounded && Player_Input.IsFire)
        {
            animator.SetBool("Shoot", true);
            rigid2D.velocity = Vector2.zero;

            if(Time.time > nextShoot)
            {
                nextShoot = Time.time + fireRate;

                GameObject bullet = playerPools.GetPolledBullet();
                bullet.transform.position = bulletLouncher.position;
                bullet.transform.rotation = bulletLouncher.rotation;
                bullet.SetActive(true);
            }
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    private void PlayerWin()
    {
        animator.SetBool("Win", true);
    }

    private void PlayerDead()
    {
        animator.SetBool("Dead", true);
    }

    private void PlayerFlip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(touchDownSensor.position, touchDownSensorRadius);
        Gizmos.DrawWireSphere(touchBoxSensor.position, boxTouchSensorRadius);
    }
}
