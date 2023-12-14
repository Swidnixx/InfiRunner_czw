using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    public ParticleSystem effect;
    public AudioClip audioEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.Instance.MagnetCollected();
            Instantiate(effect, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySfx(audioEffect);
        }
    }
}
