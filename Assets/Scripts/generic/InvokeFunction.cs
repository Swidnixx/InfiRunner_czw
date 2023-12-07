using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeFunction : MonoBehaviour
{
    public UnityEvent functionToInvoke;

    public void Invoke()
    {
        functionToInvoke.Invoke();
    }
}
