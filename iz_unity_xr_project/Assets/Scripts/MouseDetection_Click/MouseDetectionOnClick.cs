using UnityEngine;
using UnityEngine.Events;

public class MouseDetectionOnClick : MonoBehaviour
{
    public UnityEvent MouseClickedOnObject;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClickedOnObject.Invoke();
        }
    }
}
