using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Korsarze : MonoBehaviour
{
    public ParticleSystem effect;
    public AudioClip sfx;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetTrigger("trigger");
            Instantiate(effect, transform.position, Quaternion.identity, transform.parent);
            collision.GetComponent<PlayerController>().Kors();
            SoundManager.Instance.PlaySfx(sfx);
        }
    }
}
