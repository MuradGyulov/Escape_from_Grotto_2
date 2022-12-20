using UnityEngine;
using UnityEngine.Events;
using YG;

public class Player_Input : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }
    public static bool IsFire { get; private set; }

    public static UnityEvent IsJump = new UnityEvent();

    private bool Is_Desctop = false;

    private void Start()
    {
        Is_Desctop = YandexGame.EnvironmentData.isDesktop;
    }

    private void Update()
    {
        if (Is_Desctop)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.W)) { IsJump.Invoke(); }
            if (Input.GetMouseButton(0)) { IsFire = true; } else { IsFire = false; }
        }      
    }

    public void MoveLeft() { Horizontal = -1; }
    public void MoveRight() { Horizontal = 1; }
    public void StopMovement() { Horizontal = 0; }
    public void Jump() { IsJump.Invoke(); }
    public void OpenFire() { IsFire = true; }
    public void StopFiring() { IsFire = false; }
}
