using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kerze : MonoBehaviour
{
    private Vector3 previousPosition;
    private float previousTime;
    bool isGrabbed;
    public ParticleSystem parts;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isGrabbed) return;

        // Aktuelle Zeit
        float currentTime = Time.time;

        // Zeitdifferenz seit dem letzten Frame
        float deltaTime = currentTime - previousTime;

        // Aktuelle Position des GameObjects
        Vector3 currentPosition = transform.position;

        // Berechne den Positionsunterschied
        Vector3 displacement = currentPosition - previousPosition;

        // Berechne die Geschwindigkeit
        Vector3 velocity = displacement / deltaTime;

        // Aktualisiere die vorherige Position und Zeit
        previousPosition = currentPosition;
        previousTime = currentTime;

        if(velocity.magnitude >= 1){
            print("bigger");
            parts.Stop();
        }
        
    }

    // Kerze greifen
    void OnTriggerEnter(Collider col){
        if(col.transform.name == "RightHandAttatchPoint"){
            transform.position = col.transform.position;
            transform.parent = col.transform;
            previousPosition = transform.position;
            previousTime = Time.time;
            isGrabbed = true;
        }
    }
}
