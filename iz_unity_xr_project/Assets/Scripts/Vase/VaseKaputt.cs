using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseKaputt : MonoBehaviour
{
    public GameObject kaputteVase;

    void OnCollisionEnter(Collision col){
        print(col.transform.name);
        print(transform.name);
        print("----------------");
        if(col.transform.tag == "Ball"){
            Instantiate(kaputteVase, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
