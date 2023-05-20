using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSprites : MonoBehaviour
{
    public GameObject[] sprites;
    GameObject picture;
    public float drawDensity = 1; // 1 jedes sekunde, 0.5 2stk / sekunde usw
    private bool doDawElement = false;
    public float camSpeed = 1;
    float cnt = 0;
    // Update is called once per frame

    void Start(){
        picture = new GameObject();
        picture.name = "Picture";
    }

    void Update()
    {   
        // Move the Camera to navigate around the canvas 
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Camera.main.transform.position += new Vector3(hor, vert, 0) * camSpeed * Time.deltaTime;

        // check if this frame should draw a element
        if(cnt >= drawDensity){
            doDawElement = true;
            cnt = 0;
            print(cnt);
        } else{
            doDawElement = false;
            cnt += Time.deltaTime;
            print(cnt);
        } 
        
        // if pressed and this frame should draw, do it
        if(Input.GetMouseButton(0)){
            if(doDawElement){
                Vector3 mousePos = Input.mousePosition;
                // mousePos.z = Camera.main.nearClipPlane;
                Vector3 castPoint = Camera.main.ScreenToWorldPoint(mousePos);
                castPoint.z = 0;
                
                int randomNumber = Random.Range(0, sprites.Length);
                GameObject newSprite = Instantiate(sprites[randomNumber], castPoint, Quaternion.identity);
                newSprite.transform.parent = picture.transform;
                // newSprite.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
       }
    }
}
