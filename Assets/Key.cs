using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject BlockWall;
    [Space(10)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particSystem;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlockWall.SetActive(false);
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        audioSource.Play();
        particSystem.Play();
        StartCoroutine(HidenGameObject());
    }

    private IEnumerator HidenGameObject()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
