using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Actions : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float runSpeed;
    [SerializeField] private float boxPooshSpeed;
    [SerializeField] private float climbingSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    [Space(10)]
    [SerializeField] private float touchDownSensorRadius;
    [SerializeField] private float boxTouchSensorRadius;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsBox;
    [Space(60)]
    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip winSound;
    [Space(10)]
    [SerializeField] private Transform touchDownSensor;
    [SerializeField] private Transform touchBoxSensor;
    [SerializeField] private Transform bulletLauncher;
    [SerializeField] private Transform sleeveLauncher;
    [Space(10)]
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private AudioSource audios;
    [SerializeField] private Collider2D boxCollid2D;
    [SerializeField] private Animator animator;

    public static UnityEvent PlayerIsWin = new UnityEvent();
    public static UnityEvent PlayerIsDead = new UnityEvent();
    public static UnityEvent DialogIsBegin = new UnityEvent();

    private Player_Pools playerPools;

    private float nextShoot;
    private bool facingRight;
    private bool isGrounded;
    private bool isPaused;
    private bool onLadders;
    private bool isTouchBox;
    [HideInInspector] public bool isStop;


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
        PlayerClimbing();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gates":
                if (!isStop)
                {
                    rigid2D.velocity = Vector2.zero;
                    isStop = true;
                    if (!audios.isPlaying) { audios.PlayOneShot(winSound); }
                    animator.SetBool("Win", true);
                    PlayerIsWin.Invoke();                   
                }
                break;
            case "Dangerous":
                if (!isStop) 
                {
                    rigid2D.velocity = Vector2.zero;
                    if (!audios.isPlaying) { audios.PlayOneShot(hitSound); }
                    rigid2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    animator.SetBool("Dead", true);
                    PlayerIsDead.Invoke();
                    isStop = true;
                }               
                break;
            case "Ladders":
                onLadders = true;
                break;
            case "Dialog Point":
                rigid2D.velocity = Vector2.zero;
                animator.SetFloat("Run", 0);
                isStop = true;
                Destroy(collision.gameObject);
                break;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        onLadders = false;
    }

    private void PlayerRun()
    {
        if (!isStop)
        {
            rigid2D.velocity = new Vector2(Player_Input.Horizontal * runSpeed, rigid2D.velocity.y);
            animator.SetFloat("Run", Mathf.Abs(Player_Input.Horizontal));

            if (Player_Input.Horizontal > 0 && facingRight)
            {
                PlayerFlip();
            }
            else if (Player_Input.Horizontal < 0 && !facingRight)
            {
                PlayerFlip();
            }
        }
    }

    private void PlayerClimbing()
    {
        if (onLadders && Player_Input.Vertical > 0)
        {
            rigid2D.gravityScale = 0;
            animator.SetFloat("Climbing", Mathf.Abs(Player_Input.Vertical));
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, climbingSpeed);
        }
        else
        {
            animator.SetFloat("Climbing", 0);
            rigid2D.gravityScale = 3;
        }
    }

    private void PlayerPush()
    {
        if (!isStop)
        {
            if (isGrounded && isTouchBox)
            {
                rigid2D.velocity = new Vector2(Player_Input.Horizontal  * boxPooshSpeed, rigid2D.velocity.y);
                animator.SetBool("Push", true);
                boxCollid2D.enabled = true;
            }
            else
            {
                animator.SetBool("Push", false);
                boxCollid2D.enabled = false;
            }
        }
    }

    private void PlayerJump()
    {
        if (isGrounded && !isPaused && !isStop)
        {
            if(!audios.isPlaying)
            {
                audios.PlayOneShot(jumpSound);
            }
            rigid2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
        }
    }

    private void PlayerShoot()
    {
        if (isGrounded && Player_Input.IsFire && !isStop)
        {
            animator.SetBool("Shoot", true);
            rigid2D.velocity = Vector2.zero;

            if(Time.time > nextShoot)
            {
                nextShoot = Time.time + fireRate;

                GameObject bullet = playerPools.GetPooledBullet();
                if(bullet != null)
                {
                    bullet.transform.position = bulletLauncher.position;
                    bullet.transform.rotation = bulletLauncher.rotation;
                    if (!audios.isPlaying) { audios.PlayOneShot(shootingSound); }
                    bullet.SetActive(true);
                }

                GameObject sleeve = playerPools.GetPooledSleeve();
                if(sleeve != null)
                {
                    sleeve.transform.position = sleeveLauncher.position;
                    sleeve.transform.rotation = sleeveLauncher.rotation;
                    sleeve.SetActive(true);
                }
            }
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    private void PlayerFlip()
    {
        if (!isStop)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            if (transform.localScale.x == 1) 
            {
                bulletLauncher.eulerAngles = new Vector3(0, 0, 0);
                sleeveLauncher.eulerAngles = new Vector3(0, 0, 12);
            }
            else if (transform.localScale.x == -1) 
            {
                bulletLauncher.eulerAngles = new Vector3(0, 180, 0);
                sleeveLauncher.eulerAngles = new Vector3(0, 180, 12);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(touchDownSensor.position, touchDownSensorRadius);
        Gizmos.DrawWireSphere(touchBoxSensor.position, boxTouchSensorRadius);
    }
}
