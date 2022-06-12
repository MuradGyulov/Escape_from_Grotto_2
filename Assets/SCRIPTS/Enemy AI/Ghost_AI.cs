using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_AI : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [Space(15)]
    [SerializeField] private Transform target;


    private bool facingRight = true;


    private void FixedUpdate()
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

    private void SlugFlip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
