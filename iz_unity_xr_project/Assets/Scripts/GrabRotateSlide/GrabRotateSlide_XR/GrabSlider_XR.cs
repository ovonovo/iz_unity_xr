using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSlider_XR : MonoBehaviour
{
    public GameObject handle;
    public Animator anim;
    ConfigurableJoint configurableJoint;
    float startPosition;
    public string animatorParameter = "Blend";
    // Start is called before the first frame update
    void Start()
    {
        configurableJoint = handle.GetComponent<ConfigurableJoint>();
        startPosition = handle.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
    }

    void UpdateAnimator(){
        // print(handle.transform.position.x);
        float limitMin = startPosition - configurableJoint.linearLimit.limit;
        float limitMax = startPosition + configurableJoint.linearLimit.limit;
        float value = Map(handle.transform.position.x, limitMin, limitMax, 0, 1);
        anim.SetFloat(animatorParameter, value);
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
