using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_PlayerController : MonoBehaviour
{
    // Controller für den Screenshot Modus
    public float gravity = 9.81f;
    public float speed = 2;

    CharacterController controller;
    Vector3 playerVelocity;

    [Header("Mouse Look")]
    public Camera cam;
    Vector3 camStartHeight;
    public Vector2 sensitivity = new Vector2(100, 100);
    public float minRotationY = -60;
    public float maxRotationY = 60;
    float xRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        camStartHeight = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        MovePlayer();
        
        if(Input.GetKeyDown(KeyCode.B)){
            TakeScreenshot();
        }
    }

    void MouseLook(){
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minRotationY, maxRotationY);

        // Drehung des Bodys nach links und rechts
        transform.Rotate(Vector3.up * mouseX);
        // Drehung der Kamera auf und ab
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }

    void MovePlayer(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool up = Input.GetKey(KeyCode.Q);
        bool down = Input.GetKey(KeyCode.E);
        bool reset = Input.GetKeyDown(KeyCode.R);

        // calculate move direction
        Vector3 move = (transform.forward * z) + (transform.right * x);

        playerVelocity = move * speed * Time.deltaTime;
        playerVelocity.y += (-gravity) * Time.deltaTime;
        controller.Move(playerVelocity);

        // rise and lower cam for screenshots
        if(up){
            cam.transform.position += Vector3.up * 0.02f;
        }else if(down){
            cam.transform.position -= Vector3.up * 0.02f;
        }
        if(reset){
            cam.transform.position = camStartHeight;
        }
    }

    void TakeScreenshot(){
        string timeStemp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot_" + timeStemp + ".png";
        // string pathToSave = Application.persistentDataPath + "/Screenshots/" + fileName;  
        string pathToSave = "Screenshots/" + fileName;  

        ScreenCapture.CaptureScreenshot(pathToSave);
        print("Took screenshot");
    }
}
