using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseDetection : MonoBehaviour
{
    public UnityEvent MouseEnter;
    public UnityEvent MouseExit;

    void OnMouseOver()
    {
        MouseEnter.Invoke();
    }

    void OnMouseExit()
    {
        MouseExit.Invoke();
    }
}
