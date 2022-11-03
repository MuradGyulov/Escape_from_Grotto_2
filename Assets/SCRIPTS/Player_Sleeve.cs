using System.Collections;
using UnityEngine;

public class Player_Sleeve : MonoBehaviour
{
    [SerializeField] private float minForce, maxForce;
    [SerializeField] private float sleeveLifetime;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidbod2D;



    private void OnEnable()
    {
        rigidbod2D.AddForce(transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
        StartCoroutine(SleeveDisabele());
    }

    private IEnumerator SleeveDisabele()
    {
        yield return new WaitForSeconds(sleeveLifetime);
        gameObject.SetActive(false);
    }
}
