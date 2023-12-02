using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celownik : MonoBehaviour
{
    public Animation eyeOpen;
  //  public Animator animatorOka;
    Animator animatorPowiek;
    Transform kiddo;

    private void Start()
    {
        animatorPowiek = GetComponent<Animator>();    
        kiddo = transform.GetChild(0);

        StartCoroutine(Move());
    }

    public void OpenEye(bool isOpen)
    {
        eyeOpen.Play();
        //animatorOka.SetBool("open", isOpen);
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
