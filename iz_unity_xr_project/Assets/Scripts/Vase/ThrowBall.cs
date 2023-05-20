using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ball;
    public Texture2D cursorTexture;
    public Vector2 sensitivity = new Vector2(100, 100);
    public float force = 10;

    float xRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(cursorTexture != null) Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto); 
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        xRot -= mouseY;

        transform.Rotate(Vector3.up * mouseX);
        transform.GetChild(0).transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);  

        if(Input.GetMouseButtonDown(0)){
            Vector3 newBallPos = transform.GetChild(0).position;
            GameObject newBall = Instantiate(ball, newBallPos, Quaternion.identity);
            newBall.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).transform.forward * force, ForceMode.Impulse);
        }
    }
}
