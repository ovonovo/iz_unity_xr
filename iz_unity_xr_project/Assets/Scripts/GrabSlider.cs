using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabSlider : MonoBehaviour
{
    public Animator anim;

    public float slideSpeed = 1;
    public float minPosX = -1f; // Minimale X-Position des Griffs
    public float maxPosX = 1f;  // Maximale X-Position des Griffs

    private Transform handle;
    private Vector3 startPos;
    private float currPos;

    private void Start()
    {
        handle = transform.GetChild(0);
        startPos = handle.localPosition;
    }

    private void OnMouseDrag()
    {
        float slidePosition = Input.GetAxis("Mouse X") * slideSpeed;

        float newPosX = Mathf.Clamp(handle.localPosition.x + slidePosition, minPosX, maxPosX);
        handle.localPosition = new Vector3(newPosX, handle.localPosition.y, handle.localPosition.z);

        currPos = Map(handle.localPosition.x, minPosX, maxPosX, 0, 1);
        anim.SetFloat("Blend", currPos);
    }

    private float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        float ClampValue = Mathf.Clamp(NewValue, NewMin, NewMax);
        return ClampValue;
    }
}
