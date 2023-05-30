using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class CameleonTongue : MonoBehaviour
{
    LineRenderer lr;
    public GameObject tongueStart;
    public GameObject tongueTip;
    public GameObject tongueTarget;
    private Transform prevTongueTarget;

    AimConstraint lookConstraint;
    ConstraintSource cSource = new ConstraintSource(); 

    public float tongueSpeed = .2f;
    [Tooltip("Ab welcher Nähe das Chamäleon nach dem Futter schnappt")]
    public float chatchDistance = 1.5f;
    public bool doShoot = false;
    bool isShooting = false;
    bool isCatchingTarget = false;
    bool disabledComponents = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        tongueTip.transform.position = tongueStart.transform.position;
        lookConstraint = tongueStart.GetComponent<AimConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootTongue();
        lr.SetPosition(0, tongueStart.transform.position);
        lr.SetPosition(1, tongueTip.transform.position);
    }

    public void ShootTongue() {
        if(tongueTarget == null) return;

        if(Vector3.Distance(tongueStart.transform.position, tongueTarget.transform.position) < chatchDistance && !isShooting && !isCatchingTarget){
                print("ishoot");
                doShoot = true;
        }

        if(isCatchingTarget && !disabledComponents){
            if(tongueTarget.GetComponent<XRGrabInteractable>() != null) {
                Destroy(tongueTarget.GetComponent<XRGrabInteractable>());
            }
            if(tongueTarget.GetComponent<Rigidbody>() != null) {
                Destroy(tongueTarget.GetComponent<Rigidbody>());
            }
            disabledComponents = true;
        }

        if(!isShooting && doShoot) {
            isShooting = true;
            doShoot = false;
        } else if(isShooting) {
            tongueTip.transform.position = Vector3.Lerp(tongueTip.transform.position, tongueTarget.transform.position, tongueSpeed);
            
            float dist = Vector3.Distance(tongueTip.transform.position, tongueTarget.transform.position);

            if(dist < .01f){
                isShooting = false;
                doShoot = false;
                isCatchingTarget = true;
            }
        } else {
            tongueTip.transform.position = Vector3.Lerp(tongueTip.transform.position, tongueStart.transform.position, tongueSpeed);
            if(isCatchingTarget){
                tongueTarget.transform.position = tongueTip.transform.position;
            }

            float dist = Vector3.Distance(tongueStart.transform.position, tongueTip.transform.position);

            if(dist < .01f && isCatchingTarget){
                isCatchingTarget = false;
                disabledComponents = false;
                Destroy(tongueTarget);
            }
        }
    }

    public void SetAimSource(){
        if(tongueTarget == null) return;
        cSource.sourceTransform = tongueTarget.transform;
    	cSource.weight = 1;
        
        lookConstraint.SetSource(0, cSource);
    }
}
