using UnityEngine;

public class Stalagmit : MonoBehaviour
{
    [SerializeField] private float delayBerforFall;
    [Space(25)]
    [SerializeField] private SpriteRenderer spriteRendere;
    [SerializeField] private PolygonCollider2D poligonCollider2D;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem stalagmitDestructionParticles;
    [SerializeField] private Stalagmit_Sensor stalagmitSensor;
    [SerializeField] private GameObject thisStalagmit;

    public void ActivateStalagmit()
    {
        stalagmitSensor.gameObject.SetActive(false);
        animator.enabled = true;
        Invoke("StalgmitFall", delayBerforFall);
    }

    private void StalgmitFall()
    {
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                spriteRendere.enabled = false;
                poligonCollider2D.enabled = false;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                stalagmitDestructionParticles.Play();
                Destroy(thisStalagmit, 1);
                break;
            case "Box":
                spriteRendere.enabled = false;
                poligonCollider2D.enabled = false;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                stalagmitDestructionParticles.Play();
                Destroy(thisStalagmit, 1);
                break;
            case "Player Bullet":
                spriteRendere.enabled = false;
                poligonCollider2D.enabled = false;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                stalagmitDestructionParticles.Play();
                Destroy(thisStalagmit, 1);
                break;
            case "Player":
                spriteRendere.enabled = false;
                poligonCollider2D.enabled = false;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                stalagmitDestructionParticles.Play();
                Destroy(thisStalagmit, 1);
                break;
        }
    }
}
