using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celownik : MonoBehaviour
{
    Animator animator;
    Transform kiddo;

    private void Start()
    {
        animator = GetComponent<Animator>();    
        kiddo = transform.GetChild(0);

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(true)
        {
            animator.SetTrigger("move");
            yield return new WaitForSeconds(Random.value * 3);
        }
    }
}
