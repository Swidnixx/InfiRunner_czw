using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem effect;

    public float atractionSpeed = 5;
    public float floatingSpeed = 1;
    public float floatDistance = 0.5f;
    bool up = true;
    float initialY;
    Transform player;

    private void Start()
    {
        initialY = transform.position.y;
        player = GameObject.FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        Floating();
        MagnetAtracting();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.Instance.CoinCollected();
            Instantiate(effect, transform.position, Quaternion.identity);
        }
    }

    void MagnetAtracting()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        bool inRange = distanceToPlayer < GameManager.Instance.magnet.MagnetDistance;
        if (GameManager.Instance.MagnetActive && inRange)
        {
            Vector2 nextPos = Vector2.MoveTowards(
                transform.position, 
                player.position,
                Time.deltaTime * atractionSpeed);
            transform.position = nextPos;
        }
    }
    void Floating()
    {
        float d = floatingSpeed * Time.deltaTime;
        d *= up ? 1 : -1;
        transform.Translate(0, d, 0);

        float limitY = up ? initialY + floatDistance : initialY - floatDistance;
        if (up && transform.position.y >= limitY)
            up = false;
        else if (!up && transform.position.y <= limitY)
            up = true;
    }
}
