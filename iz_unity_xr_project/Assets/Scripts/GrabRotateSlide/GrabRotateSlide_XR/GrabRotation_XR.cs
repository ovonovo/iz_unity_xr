using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabRotation_XR : MonoBehaviour
{
    [Tooltip("Object to get rotation from")]
    public HingeJoint hingeJoint;

    public UnityEvent leverPositionON;
    public UnityEvent leverPositionOFF;

    // Update is called once per frame
    void Update()
    {
        float minPos = hingeJoint.limits.min;
        float maxPos = hingeJoint.limits.max;

        if(hingeJoint.angle <= minPos+1){
            leverPositionON.Invoke();
            print("ON");
        }else if(hingeJoint.angle >= maxPos-1){
            leverPositionOFF.Invoke();
            print("OFF");
        }

        if(Input.GetKeyDown(KeyCode.P)) print(hingeJoint.angle);
    }
}
