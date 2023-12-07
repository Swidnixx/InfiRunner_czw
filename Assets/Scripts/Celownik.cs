using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celownik : MonoBehaviour
{
    //Settings
    public float leftBorder = -10, rightBorder = 10;
    public float moveSpeed = 1;
    public bool headingRight = true;
    public float floatingHeight = 1;

    public GameObject maskaOka;
    Animator animatorOka;

    Animator animatorPowiek;
    Transform kiddo;

    bool eyeOpen;

    private void Start()
    {
        startY = transform.position.y;

        animatorPowiek = GetComponent<Animator>();
        animatorOka = maskaOka.GetComponent<Animator>();
        kiddo = transform.GetChild(0);

        OpenEye(false);
        AnimationToggle();

        StartCoroutine(Move());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OpenEye(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenEye(false);
        }
    }

    float startY;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(Time.time) * floatingHeight);

        if (headingRight)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            if (transform.position.x > rightBorder)
                headingRight = false;
        }
        else
        {
            transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
            if (transform.position.x < leftBorder)
                headingRight = true;
        }
    }

    public void OpenEye(bool isOpen)
    {
        if (isOpen)
            maskaOka.SetActive(true);

        eyeOpen = isOpen;
        animatorOka.SetBool("open", isOpen);
    }

    public void AnimationToggle()
    {
        maskaOka.SetActive(eyeOpen);
    }

    IEnumerator Move()
    {
        while(true)
        {
            animatorPowiek.SetTrigger("move");
            yield return new WaitForSeconds(Random.value * 3);
        }
    }
}
