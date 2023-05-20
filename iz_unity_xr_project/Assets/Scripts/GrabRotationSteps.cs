using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotationSteps : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float minAngle = 90f;
    public float maxAngle = 270f;
    public int numSteps = 5;

    private float stepSize;
    private float targetAngle;
    private float currentAngle;

    private void Start()
    {
        stepSize = (maxAngle - minAngle) / (float)numSteps;
        currentAngle = minAngle;
        targetAngle = minAngle;
        UpdateRotation();
    }

    private void OnMouseDrag()
    {
        float zRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        targetAngle -= zRotation;

        if (targetAngle < minAngle)
            targetAngle = minAngle;
        else if (targetAngle > maxAngle)
            targetAngle = maxAngle;

        int newStep = Mathf.RoundToInt((targetAngle - minAngle) / stepSize);
        targetAngle = minAngle + newStep * stepSize;

        UpdateRotation();
    }

    private void UpdateRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetAngle);
        currentAngle = targetAngle;
    }
}
