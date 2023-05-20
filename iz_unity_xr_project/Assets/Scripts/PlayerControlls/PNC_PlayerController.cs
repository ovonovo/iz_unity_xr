using UnityEngine;

public class PNC_PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float gravity = 9.81f;
    public float speed = 2;
    public Transform targetPoint;
    private Vector3 targetPointXZ;

    public Transform targetMarker;

    CharacterController controller;
    Vector3 playerVelocity;

    public float minTargetDistance = 0.5f;
    float currTargetDistance;

    bool searchTarget = false;

    [Header("Mouse Look")]
    public Camera cam;
    public Vector2 sensitivity = new Vector2(100, 100);
    public float minRotationY = -60;
    public float maxRotationY = 60;

    float xRot = 0;

    bool mouseOverGround = false;
    bool mouseOverObject = false;
    public bool mouseDraggingFromObject = false;

    public Texture2D cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if(cursorTexture != null) Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto); 
    }

    // Update is called once per frame
    void Update()
    {
        if(!mouseOverObject && !mouseDraggingFromObject){
            // Default Cursor 
            if(cursorTexture != null) Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto); 
        }

        if(Input.GetMouseButton(1)){ //right mouse button
            Cursor.lockState = CursorLockMode.Locked;
            searchTarget = false;
            targetPoint.GetChild(0).gameObject.SetActive(false);
            MouseLook();
        } else {
            Cursor.lockState = CursorLockMode.None;
            if(searchTarget) MovePlayer();
            else if(!searchTarget) SetMarker();
    
            MouseWheelMovement();
        }
    }

    void MovePlayer(){
        // if (controller.isGrounded)  print("CharacterController is grounded");
        targetPointXZ = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);
        currTargetDistance = Vector3.Distance(transform.position, targetPointXZ);

        if(currTargetDistance > minTargetDistance){
            targetPoint.GetChild(0).gameObject.SetActive(true);
            // print(currTargetDistance);
            // LookAt() ist zu ruppig, darum ersetzt mit SmoothLookAt()
            // transform.LookAt(targetPointXZ);
            SmoothLookAt(targetPointXZ);

            Vector3 forward = transform.TransformDirection(Vector3.forward); // local Space to World Space
            playerVelocity = forward * speed;
            playerVelocity.y -= gravity;
        } else{
            searchTarget = false;
            playerVelocity = new Vector3(0, -gravity, 0);
            targetPoint.GetChild(0).gameObject.SetActive(false);
        }
        controller.Move(playerVelocity * Time.deltaTime);

        if(Input.GetMouseButtonDown(0)){
            searchTarget = false;
        }
    }

    void MouseLook(){
        // Wenn die rechte Maustaste gedrückt wird, möchten wir uns umsehen
        // auf der vertikalen Achse drehen wir die Cam, auf der horizontalen den Player
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minRotationY, maxRotationY);

        // print("mouse is looking");
        // dreht den Player nach links und rechts
        transform.Rotate(Vector3.up * mouseX);
        // dreht die Cam nach oben und unten
        cam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f); 
    }

    void MouseWheelMovement(){
        // scroll faster on pressing shift
        float scrollspeed;
        if (Input.GetKey(KeyCode.LeftShift)) scrollspeed = speed * 5;
        else scrollspeed = speed;
        
        // move foward and backwards on scroll
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            Vector3 forward = transform.TransformDirection(Vector3.forward); // local Space to World Space
            playerVelocity = forward * scrollspeed;
            playerVelocity.y -= gravity;
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0){
            Vector3 forward = transform.TransformDirection(Vector3.forward); // local Space to World Space
            playerVelocity = forward * -scrollspeed;
            playerVelocity.y -= gravity;
        }else{
            playerVelocity = new Vector3(0, -gravity, 0);
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void SetMarker(){
        if(mouseOverGround && !mouseDraggingFromObject){
            Vector3 mousePos = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(Input.GetMouseButton(0)){
                targetMarker.GetChild(0).gameObject.SetActive(true);
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    targetMarker.transform.position = hit.point;
                }
            }else if(Input.GetMouseButtonUp(0)){
                targetPoint.transform.position = targetMarker.transform.position;
                targetMarker.GetChild(0).gameObject.SetActive(false);
                searchTarget = true;
            }
        }
    }

    void SmoothLookAt(Vector3 target){
        var targetRotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void MouseOverObject(bool state){
        mouseOverObject = state;
    }
    public void MouseDragOverObject(bool state){
        mouseDraggingFromObject = state;
    }
    public void MouseOverGround(bool state){
        mouseOverGround = state;
    }
}
