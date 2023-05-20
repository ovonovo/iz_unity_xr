using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class SetupScene : MonoBehaviour
{
    // public InputActionMap XRIHead;
    public bool isVR;
    [Header("Objects to be Handled depending on XR/DESKTOP")]
    [Header(" ")]
    [Tooltip("Objects for XR")]
    public Transform[] XRObjects;
    [Tooltip("Objects for Desktop-Version")]
    public Transform[] PCObjects;

    void Awake()
    {
        if(FindObjectOfType<CheckHMD>() != null){
            isVR = FindObjectOfType<CheckHMD>().isVR;
        }
        
        if(isVR){
            enableXRObjects();
            disablePCPbjects();
        }else{
            enablePCObjects();
            disableXRObjects();
        }
    }

    private void enablePCObjects(){
        foreach(Transform child in PCObjects){
            child.gameObject.SetActive(true);
        }
    }
    private void enableXRObjects(){
        foreach(Transform child in XRObjects){
            child.gameObject.SetActive(true);
        }
    }
    private void disablePCPbjects(){
        foreach(Transform child in PCObjects){
            child.gameObject.SetActive(false);
        }
    }
    private void disableXRObjects(){
        foreach(Transform child in XRObjects){
             child.gameObject.SetActive(false);
        }
    }
}
