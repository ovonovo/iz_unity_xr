using UnityEngine;

public class WASD_PlayerController : MonoBehaviour
{

    public float gravity = -9.81f;
    public CharacterController characterController;

    [Header("Mouse Look")]
    Camera cam;
    Vector3 camStartHeight;
    public Vector2 sensitivity = new Vector2(100f, 100f);
    public Vector2 clampRotationY = new Vector3(-60f, 60f);
    float xRot = 0f;
    public float speed = 20;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = Transform.FindObjectOfType<Camera>();
        camStartHeight = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        MovePlayer();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        // wenn die Maus Y einen ungewöhnlichen Wert hat, der dazu führt, dass die Kamera nach unten schaut,
        // wird der Wert auf 0 gesetzt. Ansonsten wird die Kamera nach oben gedreht.
        if (mouseY > 10) mouseY = 0;
        else if (mouseY < -10) mouseY = 0;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, clampRotationY.x, clampRotationY.y);

        // Drehung des Bodys nach links und rechts
        transform.Rotate(Vector3.up * mouseX);
        // Drehung der Kamera auf und ab
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }


    void MovePlayer()
    {
        // read the inputs for movement
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        bool up = Input.GetKey(KeyCode.Q);
        bool down = Input.GetKey(KeyCode.E);
        bool reset = Input.GetKey(KeyCode.R);

        // calculate the direction of the player
        Vector3 move = (transform.forward * ver) + (transform.right * hor);
        print(move);
        Vector3 playerVelocity = move * speed * Time.deltaTime;
        playerVelocity.y += (gravity) * Time.deltaTime;

        print(playerVelocity);
        
        characterController.Move(playerVelocity);

        if (up)
        {
            cam.transform.position += transform.up * 0.02f;
        }
        else if (down)
        {
            cam.transform.position -= transform.up * 0.02f;
        }

        if (reset)
        {
            cam.transform.localPosition = camStartHeight;
        }
    }
}
