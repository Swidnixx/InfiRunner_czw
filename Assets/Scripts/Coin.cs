using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 1;
    public float floatDistance = 0.5f;
    bool up = true;
    float initialY;

    private void Start()
    {
        initialY = transform.position.y;
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
        }
    }

    //void MagnetAtracting()
    //{

    //}
    void Floating()
    {
        float d = speed * Time.deltaTime;
        d *= up ? 1 : -1;
        transform.Translate(0, d, 0);

        float limitY = up ? initialY + floatDistance : initialY - floatDistance;
        if (up && transform.position.y >= limitY)
            up = false;
        else if (!up && transform.position.y <= limitY)
            up = true;
    }
}
