using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenFirework : MonoBehaviour
{
    public GameObject pen;
    public bool makeFirework;
    public int penAmount = 1;
    public float rangeAngle = 10;
    public float force = 10;
    public Color[] penColors;

    GameObject pa;

    void Start()
    {
        pa = new GameObject("pens");
    }
    // Update is called once per frame
    void Update()
    {
        if(makeFirework){
            MakeFirework();
            makeFirework = false;
        }
    }

    void MakeFirework(){
        for(int i = 0; i < penAmount; i++){
            // Create new pen
            var instVector= new Vector3(-90, 0, 0);
            var newPen = Instantiate(pen, transform.position, Quaternion.Euler(instVector));
            newPen.transform.parent = pa.transform;
            newPen.name = "pen_"+i;
            // make the pens a random color of a preset color set
            newPen.GetComponent<MeshRenderer>().material.color = penColors[Random.Range(0, penColors.Length)];
            // shoot the pen in a random angle in the air
            var ran_x = Random.Range(-rangeAngle, rangeAngle);
            var ran_y = Random.Range(-rangeAngle, rangeAngle);
            var ran_z = Random.Range(-rangeAngle, rangeAngle);
            var forceVector = new Vector3(ran_x, ran_y, ran_z);
            newPen.GetComponent<Rigidbody>().AddForce(forceVector.normalized * force, ForceMode.Impulse);
        }
    }
}
