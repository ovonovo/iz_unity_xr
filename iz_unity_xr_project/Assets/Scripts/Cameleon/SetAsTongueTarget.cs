using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsTongueTarget : MonoBehaviour
{
    CameleonTongue tongue;
    public bool setTongueTarget;
    // Start is called before the first frame update
    void Start()
    {
        tongue = FindObjectOfType<CameleonTongue>();
    }

    // Update is called once per frame
    void Update()
    {
        if(setTongueTarget){
            SetAsTarget();
        }
    }

    public void SetAsTarget(){
        tongue.tongueTarget = gameObject;
        tongue.SetAimSource();
        setTongueTarget = false;
    }
}
