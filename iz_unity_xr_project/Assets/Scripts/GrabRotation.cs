using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotation : MonoBehaviour
{

    public float rotationSpeed = 1;
    public float minAngle = 90;
    float currentPosition = 0;
    // 360 = 100%
    // ich hab also zur Verfügung 360-minAngle und abs(360-maxAngle)
    // was da rauskommt ist meine neue 100%
    void OnMouseDrag()
    {
        float maxAngle = 360-minAngle;
        float z_rotation = Input.GetAxis("Mouse X") * rotationSpeed;
        currentPosition += z_rotation;
        
        print(currentPosition);
        float currZrot = transform.rotation.eulerAngles.z;
        transform.Rotate(Vector3.forward, -z_rotation);

        if(currZrot > minAngle && currZrot < 180){
           transform.rotation = Quaternion.Euler(0,0,minAngle);
        }
        if(currZrot < maxAngle && currZrot > 180){
           transform.rotation = Quaternion.Euler(0,0,-minAngle);
        }
    }
}
