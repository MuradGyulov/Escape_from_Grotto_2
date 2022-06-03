using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmit : MonoBehaviour
{
    [SerializeField] private float delayBerforFall;
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private PolygonCollider2D PC;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Animator AN;
    [SerializeField] private ParticleSystem PS;
    [SerializeField] private Stalagmit_Sensor SS;
    [SerializeField] private GameObject thisStalagmit;

    public void StalagmitActivate()
    {
        SS.gameObject.SetActive(false);
        AN.enabled = true;
        Invoke("StalgmitFall", delayBerforFall);
    }

    private void StalgmitFall()
    {
        RB.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SR.enabled = false;
        PC.enabled = false;
        RB.bodyType = RigidbodyType2D.Static;
        PS.Play();
        Destroy(thisStalagmit, 1);
    }
}
