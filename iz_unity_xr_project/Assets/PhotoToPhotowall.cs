using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoToPhotowall : MonoBehaviour
{
    GameObject photoWall;
    Vector3 targetPosition;
    public float timeToWait = 5;
    public float photoSpeed = 1;
    float timer = 0;
    bool didMove = false;
    Photowall pWall;

    void Start(){
        photoWall = GameObject.Find("PhotoWall"); // Name of the empty with the Photowall.cs
        pWall = photoWall.GetComponent<Photowall>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < timeToWait){
            timer += Time.deltaTime;
        }else{
            MoveToPosition();
        }
    }

    void MoveToPosition(){
        if(!didMove) LookForPosition();
        
        transform.parent = Photowall.photos.transform;
        transform.position = Vector3.Lerp(transform.position, targetPosition, photoSpeed);
    }

    void LookForPosition(){
       
        int cnt = 0;
        for(int i = 0; i < pWall.assignedPhoto.Length; i++){
            if(pWall.assignedPhoto[i] == false){
                targetPosition = photoWall.transform.GetChild(i).position;
                pWall.assignedPhoto[i] = true;
                print("sadasd");
                didMove = true;
                break;
            }
            cnt++;
        }
        
        if(cnt >= pWall.assignedPhoto.Length){
            for(int i = 0; i < pWall.assignedPhoto.Length; i++){
                pWall.assignedPhoto[i] = false;
            }
            // Destroy(Photowall.photos.transform.GetChild(0).gameObject);
            targetPosition = photoWall.transform.GetChild(0).position;
        }
    }
}
