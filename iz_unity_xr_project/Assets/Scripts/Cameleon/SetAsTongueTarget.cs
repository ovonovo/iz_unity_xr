using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsTongueTarget : MonoBehaviour
{
    CameleonTongue tongue;
    public bool setAsTongueTarget;
    // Start is called before the first frame update
    void Start()
    {
        tongue = FindObjectOfType<CameleonTongue>();
    }

    // Update is called once per frame
    void Update()
    {
        if(setAsTongueTarget){
            tongue.tongueTarget = gameObject;
            tongue.SetAimSource();
            setAsTongueTarget = false;
        }
    }
}
