using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class grabHandMotion : MonoBehaviour
{
    public InputActionReference grabAction;
    public InputActionReference pointAction;

    public Animator motionAnimator;
    public string pointString = "Blend";
    public string grabBool = "Grab";

    // Start is called before the first frame update
    void Start()
    {
        grabAction.action.started += doGrabPose;
        grabAction.action.canceled += doIdlePose;
        // float triggerValue = grabAction.action.ReadValue as float;
    }

    void Update(){
        motionAnimator.SetFloat(pointString, pointAction.action.ReadValue<float>()); 
    }
    
    void doGrabPose(InputAction.CallbackContext obj){
        motionAnimator.SetBool(grabBool, true);
    }
    void doIdlePose(InputAction.CallbackContext obj){
        motionAnimator.SetBool(grabBool, false);
    }

}
