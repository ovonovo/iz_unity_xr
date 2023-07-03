using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Player : MonoBehaviour
{
    public enum MOVEMODE{Rigidbody, Kinematic, CharacterController};
    public MOVEMODE moveMode;
    float gravity = 9.81f;
    public float speed = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(moveMode == MOVEMODE.Rigidbody) moveRigid(x, z);
        else if (moveMode == MOVEMODE.Kinematic) moveKinematic(x, z);
        else if (moveMode == MOVEMODE.CharacterController) moveCharacterController(x, z);
    }

    void moveRigid(float x, float z){
        GetComponent<Rigidbody>().velocity = new Vector3(x, GetComponent<Rigidbody>().velocity.y, z) * speed * Time.deltaTime;
    }

    void moveKinematic(float x, float z){
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }

    void moveCharacterController(float x, float z){
        Vector3 velocity = new Vector3(x, -gravity, z) * speed * Time.deltaTime;
        GetComponent<CharacterController>().Move(velocity);
    }
}
