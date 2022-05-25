using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Input : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }
    public static bool IsFire { get; private set; }


    public static UnityEvent IsJump = new UnityEvent();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { IsJump.Invoke(); }       
    }

    private void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0)) { IsFire = true; }else { IsFire = false; }
    }
}
