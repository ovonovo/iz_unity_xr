using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    Canvas canvas;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>(); 
        cam = Camera.main;
        canvas.worldCamera = cam;
    }
}
