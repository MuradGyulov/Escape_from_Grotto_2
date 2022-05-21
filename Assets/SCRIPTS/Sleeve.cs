using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeve : MonoBehaviour
{
    [SerializeField] private float minForce, maxForce;
    [SerializeField] private float sleeveLifetime;
    [Space(20)]
    [SerializeField] private Rigidbody2D rigidbod2D;


    private void OnEnable()
    {
        float randomForce = Random.Range(minForce, maxForce);
        rigidbod2D.AddForce(transform.up * randomForce, ForceMode2D.Impulse);
        Invoke("SleeveDeactivate", sleeveLifetime);
    }

    private void SleeveDeactivate()
    {
        gameObject.SetActive(false);
    }
}
