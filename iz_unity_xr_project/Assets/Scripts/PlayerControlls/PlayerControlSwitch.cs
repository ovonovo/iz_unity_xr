using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(WASD_PlayerController))]
[RequireComponent(typeof(PNC_PlayerController))]
public class PlayerControlSwitch : MonoBehaviour
{
    public bool screenshotMode = false;
    PNC_PlayerController pnc;
    WASD_PlayerController wasd;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)){
            screenshotMode = !screenshotMode;
        }

        pnc = GetComponent<PNC_PlayerController>();
        wasd = GetComponent<WASD_PlayerController>();

        if(screenshotMode){
            wasd.enabled = true;
            pnc.enabled = false;
        }else{
            wasd.enabled = false;
            pnc.enabled = true;
        }
    }
}
