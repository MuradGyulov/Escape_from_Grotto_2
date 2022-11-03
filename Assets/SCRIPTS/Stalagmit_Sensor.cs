using UnityEngine;

public class Stalagmit_Sensor : MonoBehaviour
{
    [SerializeField] private Stalagmit stalagmit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stalagmit.ActivateStalagmit();
        }
    }
}
