using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotationSwitch : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float minAngle = 90f;
    public float maxAngle = 270f;
    public bool isSwitchedOn = false;

    private float initialZRotation;

    private void Start()
    {
        initialZRotation = transform.rotation.eulerAngles.z;
    }

    private void OnMouseDrag()
    {
        float zRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float newZRotation = Mathf.Clamp(transform.rotation.eulerAngles.z - zRotation, minAngle, maxAngle);

        if (isSwitchedOn)
        {
            if (newZRotation < initialZRotation + 180f)
            {
                newZRotation = minAngle;
                isSwitchedOn = false;
            }
        }
        else
        {
            if (newZRotation > initialZRotation + 180f)
            {
                newZRotation = maxAngle;
                isSwitchedOn = true;
            }
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newZRotation);
    }
}
