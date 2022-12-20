using UnityEngine;

public class Ghost_AI : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private float targetDetectionRadius;
    [SerializeField] private float targetPursitRadius;
    [Space(28)]
    [SerializeField] private LayerMask whoIsTarget;
    [Space(10)]
    [SerializeField] private Transform targetDetectionSensorPointer;

    private Transform target;

    private bool facingRight = true;
    private bool targetCaptured;
    private bool playerIsDeadOrWin = false;

    private void Start()
    {
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
                targetCaptured = Physics2D.OverlapCircle(targetDetectionSensorPointer.position, targetDetectionRadius, whoIsTarget);
            }
            else if (targetCaptured = Physics2D.OverlapCircle(targetDetectionSensorPointer.position, targetPursitRadius, whoIsTarget))
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
}
