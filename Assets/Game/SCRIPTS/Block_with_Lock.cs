using System.Collections;
using UnityEngine;
using YG;

public class Block_with_Lock : MonoBehaviour
{
    [SerializeField] private GameObject BlockWall;
    [Space(10)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particSystem;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audioSource.volume = YandexGame.savesData.soundsVolume;
            BlockWall.SetActive(false);
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            audioSource.Play();
            particSystem.Play();
            StartCoroutine(HidenGameObject());
        }
    }

    private IEnumerator HidenGameObject()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
